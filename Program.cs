using System;
using System.Collections.Generic;
using System.IO;

namespace Biblioteczka
{
    class Program
    {
        static void Main(string[] args)
        {

            string command = "";
            List<Book> bookList = new List<Book>();

            string[] fBooks = File.ReadAllLines("books.txt");

            if (fBooks.Length > 0)
            {

                for (int i = 0; i < fBooks.Length; i++)
                {
                    string[] book = fBooks[i].Split(';');  // split book elements (title, author etc)

                    if (book.Length == 5)
                    {
                        string title = book[0];
                        string author = book[1];
                        string type = book[2];
                        int numPages = int.Parse(book[3]);
                        string releaseDate = book[4];

                        bookList.Add(new Book(title, author, type, numPages, releaseDate));
                    }
                }
            }


            // main program loop
            do
            {
                Console.WriteLine("\nMENU:");
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. Show books ({0})", bookList.Count);
                Console.WriteLine("3. Save book list");
                Console.WriteLine("4. Find a book");
                Console.WriteLine("5. Remove book from list");
                Console.WriteLine("'exit' for exit");
                Console.Write("Option: ");
                command = Console.ReadLine();

                if (command == "1")
                {
                    Console.Write("\nTitle: ");
                    string title = Console.ReadLine();
                    Console.Write("Author: ");
                    string author = Console.ReadLine();
                    Console.Write("Type: ");
                    string type = Console.ReadLine();
                    Console.Write("Release year: ");
                    string release = Console.ReadLine();
                    Console.Write("Number of pages: ");
                    int pages = int.Parse(Console.ReadLine());

                    bookList.Add(new Book(title, author, type, pages, release));

                    Console.WriteLine("Book added.");
                }
                else if (command == "2")
                {
                    Console.WriteLine();
                    foreach (var book in bookList)
                    {
                        Console.WriteLine(book.BookInfo());
                    }
                }
                else if (command == "3")
                {
                    List<string> s = new List<string>();
                    foreach (var book in bookList)
                    {
                        string temp = $"{book.Title};{book.Author};{book.Type};{book.NumPages};{book.ReleaseDate}";
                        s.Add(temp);
                    }

                    File.WriteAllLines("books.txt", s);
                    Console.WriteLine("File saved."); 

                }
                else if (command == "4")
                {
                    Console.WriteLine("\nTo find a specific book, please type 'author stephen king' or 'title carrie' or 'type thriller' etc: ");
                    string a = Console.ReadLine();
                    int count = 0;
                    List<Book> result = new List<Book>();

                    string[] search = a.Split(' ', 2);
                    if (search.Length > 1)
                    {
                        switch (search[0].ToLower())
                        {
                            case "author":
                                foreach(var book in bookList)
                                {
                                    if (book.Author.ToLower() == search[1].ToLower())
                                    {
                                        result.Add(book);
                                        count++;
                                    }
                                }
                                break;

                            case "title":
                                foreach (var book in bookList)
                                {
                                    if (book.Title.ToLower() == search[1].ToLower())
                                    {
                                        result.Add(book);
                                        count++;
                                    }
                                }
                                break;
                            case "type":
                                foreach (var book in bookList)
                                {
                                    if (book.Type.ToLower() == search[1].ToLower())
                                    {
                                        result.Add(book);
                                        count++;
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine("Bad input.");
                                break;
                        }
                    }


                    if (count == 0)
                    {
                        Console.WriteLine($"Sorry, no books found for '{a}'.");
                    }
                    else
                    {
                        foreach(var r in result)
                        {
                            Console.WriteLine(r.BookInfo());
                        }
                    }
                }
                else if (command == "5")
                {
                    Console.Write("Title of book you want to remove: ");
                    string t = Console.ReadLine();
                    int count = 0;

                    for (int i = 0; i < bookList.Count; i++)
                    {
                        if (bookList[i].Title.ToLower() == t.ToLower())
                        {
                            bookList.RemoveAt(i);
                            count++;
                        }
                    }

                    Console.WriteLine($"{count} book(s) removed.");
                }
                else if (command == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Choose option 1, 2 etc or type 'exit'.");
                }

            } while (command != "exit");

        }
    }


    public class Book
    {
        public int NumPages;
        public string Author, Title, Type, ReleaseDate;
        
        public Book(string title, string author, string type, int pages, string releaseDate)
        {
            Title = title;
            Author = author;
            Type = type;
            NumPages = pages;
            ReleaseDate = releaseDate;
        }

        public string BookInfo()
        {
            //string s = $"'{Title}', a {Type} written by {Author} has {NumPages} pages and was released in {ReleaseDate}.";
            string s = $"'{Title}' ({Type}), {Author}, {NumPages} pages, year: {ReleaseDate}.";
            return s;
        }
    }
}
 

