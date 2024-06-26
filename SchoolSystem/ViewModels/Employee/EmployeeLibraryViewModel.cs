﻿using SchoolSystem.Models;
using SchoolSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SchoolSystem.ViewModels.Employee
{
    public class EmployeeLibraryViewModel : ViewModelBase
    {
        // Fields
        private string _bookName;
        private List<BookModel> _allBooks;

        private BookRepository bookRepository;

        // Properties
        public string BookName
        {
            get
            {
                return _bookName;
            }
            set
            {
                _bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }

        public List<BookModel> AllBooks
        {
            get
            {
                return _allBooks;
            }
            set
            {
                _allBooks = value;
                OnPropertyChanged(nameof(AllBooks));
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand DeleteBookCommand { get; }

        public EmployeeLibraryViewModel()
        {
            bookRepository = new BookRepository();

            AllBooks = [.. bookRepository.GetAllBooks().OrderBy(book => book.Name)];

            DeleteBookCommand = new ViewModelCommand(ExecuteDeleteBookCommand);
            AddBookCommand = new ViewModelCommand(ExecuteAddBookCommand, CanExecuteAddBookCommand);
        }

        private bool CanExecuteAddBookCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(BookName) && BookName.Length > 3)
            {
                return true;
            }

            return false;
        }

        private void ExecuteAddBookCommand(object obj)
        {
            bookRepository.AddBook(BookName);
            BookName = "";
            AllBooks = [.. bookRepository.GetAllBooks().OrderBy(book => book.Name)];
        }

        private void ExecuteDeleteBookCommand(object obj)
        {
            Guid bookId = (Guid)obj;

            bookRepository.DeleteBook(bookId);

            AllBooks = [.. bookRepository.GetAllBooks().OrderBy(book => book.Name)];
        }
    }
}
