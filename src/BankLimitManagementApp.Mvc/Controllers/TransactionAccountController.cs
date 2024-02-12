using Microsoft.AspNetCore.Mvc;
using BankLimitManagementApp.Domain.Entities;
using BankLimitManagementApp.Domain.Repositories;
using BankLimitManagementApp.Mvc.MappingViewModels;
using BankLimitManagementApp.Mvc.InputModels;
using BankLimitManagementApp.Domain.Services;

namespace BankLimitManagementApp.Mvc.Controllers
{
    public class TransactionAccountController(
        ITransactionAccountRepository transactionAccountRepository,
        IBankAccountRepository bankAccountRepository,
        ITransactionService transactionService) : Controller
    {
        private readonly ITransactionAccountRepository _transactionAccountRepository = transactionAccountRepository;
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
        private readonly ITransactionService _transactionService = transactionService;

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionAccountRepository.GetAllTransactions();

            var transactionsViewModel = transactions.ConvertTransactionAccountViewModel();

            return View(transactionsViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var transactionAccount = await _transactionAccountRepository.GetTransactionById(id);

            if (transactionAccount == null) return NotFound();

            var viewModelTransactionDetails = transactionAccount.ConvertTransactionAccountViewModelById();

            return View(viewModelTransactionDetails);
        }

        public async Task<IActionResult> Create()
        {
            var bankAccounts = await _bankAccountRepository.GetAllBankAccounts(null);

            var accountsViewModel = bankAccounts.ConvertBankAccountViewModel();

            var input = new TransactionAccountInputModel();
            input.BankAccounts = accountsViewModel;

            return View(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionAccountInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var account = await _bankAccountRepository.GetAccountByIdAsync(inputModel.BankAccountId);

            if (account == null) return BadRequest("Conta não existe!!");

            var transactionAccount = new TransactionAccount(inputModel.BankAccountId, inputModel.Value);

            var isValid = _transactionService.CheckLimitIsValid(account, transactionAccount);

            if (!isValid)
            {
                transactionAccount.SetTransactionDenied();

                await _transactionAccountRepository.AddTransaction(transactionAccount);

                return RedirectToAction("Index");
            }

            transactionAccount.SetTransactionApproved();

            await _transactionAccountRepository.AddTransaction(transactionAccount);

            _transactionService.DebitValueAccount(account, transactionAccount);

            _bankAccountRepository.UpdateLimitAccount(account);

            return RedirectToAction("Finally", new { account.Id });
        }

        public async Task<IActionResult> Finally(int id)
        {
            var account = await _bankAccountRepository.GetAccountByIdAsync(id);

            var finallyViewModel = account.ConvertFinallyViewModel();

            return View(finallyViewModel);
        }

    }
}

