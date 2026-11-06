// Банкомат:


// Функциональные требования:
// 1. Снимать деньги
// 2. Поплнять деньги
// 3. Узнать баланс счета или кол-во денег в банкомате
// 4. Перевести средства на другой счет

// Нефункциональные требования:
// 1. Операция должна проходить не более чем за 10 секунд
// 2. На счету может лежать не более 1 миллиона

// LINQ
// FirstOrDefault(x => x.Key == newAccount)

using ATM.Models;
using ATM.Models.Exceptions;
using System.Text.RegularExpressions;

const string GLOBAL_PASSWORD = "123";
List<Account> accounts = new();

var bankomats = new List<Bankomat>();
var location = Console.ReadLine();
if (location == null)
{
    throw new NonValidCountryException("Недопступная страна");

}
var typedCurrency = Console.ReadLine();
if (string.IsNullOrEmpty(typedCurrency))
{
    throw new NonValidCurrencyException("Недоступная валюта");
}

var bankomat = new Bankomat(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, location, typedCurrency);
bankomats.Add(bankomat);

var wallet = new Wallet(Guid.NewGuid(), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, 1000, Currency.RUB);


wallet.ChangeNominal(1023);
wallet.Test();

Console.WriteLine(wallet.Nominal);
Console.WriteLine(wallet.DateOfCreation);

wallet.ChangeNominal(3000);

Console.WriteLine(wallet.Nominal);
Bankomat currentBankomat;
Account currentAccount;
string typedPassword;
while (true)
{
    Console.WriteLine("Банкомат богдана");

    Console.WriteLine("1. Снять деньги");
    Console.WriteLine("2. Пополнить деньги");
    Console.WriteLine("3. Перевести деньги");
    Console.WriteLine("4. Узнать баланс");
    Console.WriteLine("5. Создать счет");
    Console.WriteLine("6. Текущие аккаунты");
    Console.WriteLine("7. Для админа");

    var input = Console.ReadLine();

    if (int.TryParse(input, out int choice))
        Console.WriteLine("Выполняется действие");

    switch (choice)
    {
        case 1:
            Console.WriteLine("Укажите счёт");
            var account = Console.ReadLine();
            currentAccount = accounts.SingleOrDefault(x => x.Login == account);
            if (currentAccount is not null)
            {
                Console.WriteLine("Введите пароль");
                typedPassword = Console.ReadLine();
                if (currentAccount.Password != typedPassword)
                {
                    Console.WriteLine("Неверный пароль");
                    break;
                }
                currentBankomat = bankomats.FirstOrDefault();
                Console.WriteLine("Введите сумму для снятия");
                var cash = Convert.ToInt32(Console.ReadLine());
               if(currentBankomat.TotalCash < cash)
                {
                    Console.WriteLine("Недостаточно денег в банкомате");
                    break;
                }
               if(currentAccount.TotalCash < cash)
                {
                    Console.WriteLine("Недостаточно денег на счёте");
                    break;
                }
                currentBankomat.ChangeTotalCash(cash, "WITHDRAWAL");

                currentAccount.ChangeTotalCash(cash, "WITHDRAWAL");
                Console.WriteLine($"Вы сняли {cash} рублей ");

            }
            else
            {
                Console.WriteLine("Неверно указан счёт");
            }
            break;
        case 2:
            Console.WriteLine("Пополнить деньги");
            Console.WriteLine("Укажите счёт");
            var acc = Console.ReadLine();
            currentAccount = accounts.SingleOrDefault(x => x.Login == acc);

            if (currentAccount is not null)
            {
                Console.WriteLine("Введите пароль");
                typedPassword = Console.ReadLine();
                if(currentAccount.Password != typedPassword)
                {
                    Console.WriteLine("Неверный пароль");
                    break;
                }
                currentBankomat = bankomats.FirstOrDefault();
                Console.WriteLine("Введите сумму для пополнения");
                var cash = Convert.ToInt32(Console.ReadLine());
                if (currentBankomat.TotalCash + cash > 1_000_000)
                {
                    Console.WriteLine("Недопустимая сумма на счету");
                    break;
                }
                currentBankomat.ChangeTotalCash(cash, "REPLENISHMENT");
                currentAccount.ChangeTotalCash(cash, "REPLENISHMENT");
                Console.WriteLine($"Вы пополнили {cash} рублей ");

            }
            else
            {
                Console.WriteLine("Неверно указан счёт");
            }
            break;
        case 3:
            Console.WriteLine("Перевести деньги");
            Console.WriteLine("Выберите исходный счёт");
            var fromAccount = Console.ReadLine();
            currentAccount = accounts.SingleOrDefault(x => x.Login == fromAccount);
            if(currentAccount is not null)
            {
                Console.WriteLine("Введите пароль");
                typedPassword = Console.ReadLine();
                if (currentAccount.Password != typedPassword)
                {
                    Console.WriteLine("Неверный пароль");
                    break;
                }
                Console.WriteLine("Выберите счёт для перевода");
                var toAccount = Console.ReadLine();
                var accountToTransfer = accounts.SingleOrDefault(x => x.Login == toAccount);
                if (accountToTransfer is not null)
                {
                    Console.WriteLine("Введите сумму для перевода");
                    var cash = Convert.ToInt32(Console.ReadLine());
                    if (currentAccount.TotalCash < cash)
                    {
                        Console.WriteLine("Недостаточно денег на счёте");
                        break;
                    }
                    currentAccount.ChangeTotalCash(cash, "WITHDRAWAL");
                    accountToTransfer.ChangeTotalCash(cash, "REPLENISHMENT");
                    Console.WriteLine($"Вы перевели {cash} рублей со счёта {fromAccount} на счёт {toAccount}");
                }
                else
                {
                    Console.WriteLine("Неверно указан счёт для перевода");
                }
            }
            else
            {
                Console.WriteLine("Неверно указан исходный счёт");
            }



            break;
        case 4:
            Console.WriteLine("Выберите счёт");
            var pa = Console.ReadLine();
            currentAccount = accounts.SingleOrDefault(x => x.Login == pa);
            if (currentAccount is not null)
            {
                Console.WriteLine("Узнать баланс");
                Console.WriteLine("Введите пароль"); 
                 typedPassword = Console.ReadLine();
                if (currentAccount.Password == typedPassword)
                {
                    Console.WriteLine($" Баланс равен {currentAccount.TotalCash}");
                }
                else
                {
                    Console.WriteLine("Неверный пароль");
                }
            }
            else
            {
                Console.WriteLine("Указаный счёт недействителен");
            }
            break;


        case 5:
            Console.WriteLine("Введите логин счета");
           
            var login = Console.ReadLine();
            currentAccount = accounts.SingleOrDefault(x => x.Login == login);
            if(currentAccount is not null)
            {
                Console.WriteLine("Такой логин уже существует");
                break;
            }
            Console.WriteLine("Введите пароль");
            var password = Console.ReadLine();
            var newAccount = new Account(Guid.NewGuid(), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, login, password);
            accounts.Add(newAccount);
            


            break;
        case 6:
            Console.WriteLine("Введите пароль");
            typedPassword = Console.ReadLine();

            while (GLOBAL_PASSWORD != typedPassword)
            {
                Console.WriteLine("Попробуйте еще раз!");
                typedPassword = Console.ReadLine();
            }

            foreach (var currentAcc in accounts)
            {
                Console.WriteLine(currentAcc);
            }

            break;
        case 7:
            Console.WriteLine("Введите пароль");
            var pass = Console.ReadLine();

            while (GLOBAL_PASSWORD != pass)
            {
                Console.WriteLine("Попробуйте еще раз!");
                pass = Console.ReadLine();
            }
            foreach ( var bankmat in bankomats)
            {
                Console.WriteLine(bankmat);
            }
            break;



        default:
            Console.WriteLine("Выбран неверный вариант! Попробуйте снова");
            break;
    }
}