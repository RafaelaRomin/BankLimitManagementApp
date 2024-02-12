using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using BankLimitManagementApp.Domain.Repositories;
using BankLimitManagementApp.Domain.Services;
using BankLimitManagementApp.Infra.Persistense;
using BankLimitManagementApp.Infra.Persistense.Repositories;
using BankLimitManagementApp.Mvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var (url, access, secret) = GetAwsConfig(builder);
var client = SetConfigDynamoDbLocal(url, access, secret, builder);
CreateTableIfNotExists(client).Wait();

builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<ITransactionAccountRepository, TransactionAccountRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
return;

async Task CreateTableIfNotExists(IAmazonDynamoDB client)
{
    var listTable = new List<string>()
    {
        "BankAccount",
        "TransactionAccount"
    };

    foreach (var tableName in listTable)
    {
        try
        {
            await client.DescribeTableAsync(tableName);
        }
        catch (ResourceNotFoundException)
        {
            var createTableRequest = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition("Id", ScalarAttributeType.N)
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement("Id", KeyType.HASH)
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };

            await client.CreateTableAsync(createTableRequest);

            var tableStatus = "CREATING";
            while (tableStatus == "CREATING")
            {
                await Task.Delay(1000);
                var response = await client.DescribeTableAsync(tableName);
                tableStatus = response.Table.TableStatus;
            }
        }
    }
}

(string? url, string? access, string? secret) GetAwsConfig(WebApplicationBuilder webApplicationBuilder)
{
    var awsConfigSection = webApplicationBuilder.Configuration.GetSection("AwsConfig");

    var s = awsConfigSection["ServiceURL"];
    var access1 = awsConfigSection["AccessKey"];
    var secret1 = awsConfigSection["SecretKey"];
    return (s, access1, secret1);
}

AmazonDynamoDBClient SetConfigDynamoDbLocal(string? url1, string? s1, string? secret2, WebApplicationBuilder builder1)
{
    var config = new AmazonDynamoDBConfig
    {
        ServiceURL = url1
    };

    var credentials = new BasicAWSCredentials(s1, secret2);
    var amazonDynamoDbClient = new AmazonDynamoDBClient(credentials, config);
    builder1.Services.AddSingleton<IAmazonDynamoDB>(amazonDynamoDbClient);

    var context = new DynamoDBContext(amazonDynamoDbClient);
    builder1.Services.AddSingleton<IDynamoDBContext>(context);
    return amazonDynamoDbClient;
}
