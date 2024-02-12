﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankLimitManagementApp.Domain.Entities;
using BankLimitManagementApp.Domain.Repositories;
using BankLimitManagementApp.Mvc.MappingViewModels;
using BankLimitManagementApp.Mvc.InputModels;

namespace BankLimitManagementApp.Mvc.Controllers
{
    public class BankAccountController(IBankAccountRepository bankAccountRepository) : Controller
    {
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;

        // GET: BankAccount
        public async Task<IActionResult> Index(string? filter)
        {
            var accounts = await _bankAccountRepository.GetAllBankAccounts(filter);

            var accountsViewModel = accounts.ConvertBankAccountViewModel();

            return View(accountsViewModel);
        }

        // GET: BankAccount/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var bankAccount = await _bankAccountRepository.GetAccountByIdAsync(id);

            if (bankAccount == null) return NotFound();

            var accountViewModelDetails = bankAccount.ConvertBankAccountViewModelById();

            return View(accountViewModelDetails);
        }

        // GET: BankAccount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccountInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var account = new BankAccount(inputModel.ClientName,
                                          inputModel.Document,
                                          inputModel.AgencyNumber,
                                          inputModel.AccountNumber,
                                          inputModel.TransactionLimit,
                                          inputModel.TotalAmount);

            await _bankAccountRepository.AddAccountAsync(account);

            return RedirectToAction("Index");
        }

        // GET: BankAccount/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var bankAccount = await _bankAccountRepository.GetAccountByIdAsync(id);

            if (bankAccount == null)  return NotFound();

            var bankAccountViewModel = bankAccount.ConvertBankAccountViewModelById();

            return View(bankAccountViewModel);
        }

        // POST: BankAccount/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBankAccountInputModel editBankAccountInputModel)
        {
            if (id != editBankAccountInputModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                BadRequest();
            }
            try
            {
                var account = await _bankAccountRepository.GetAccountByIdAsync(id);

                if (account == null) return NotFound();

                account.UpdateTransactionLimit(editBankAccountInputModel.TransactionLimit);

                _bankAccountRepository.UpdateLimitAccount(account);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bankAccount = await _bankAccountRepository.GetAccountByIdAsync(id);

            if (bankAccount == null) return NotFound();

            var bankAccountViewModel = bankAccount.ConvertBankAccountViewModelById();

            return View(bankAccountViewModel);
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankAccount = await _bankAccountRepository.GetAccountByIdAsync(id);

            if (bankAccount != null)
            {
                await _bankAccountRepository.DeleteAccountAsync(bankAccount);
            }

            return RedirectToAction("Index");
        }
    }
}
