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

const string GLOBAL_PASSWORD = "123456789";
Dictionary<string, decimal> accounts = new()
{
    { "test", 1234 }
};

var wallet = new Wallet(Guid.NewGuid(), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, 1000, Currency.RUB);

wallet.ChangeNominal(1023);
wallet.Test();

Console.WriteLine(wallet.Nominal);
Console.WriteLine(wallet.DateOfCreation);

wallet.ChangeNominal(3000);

Console.WriteLine(wallet.Nominal);

while (true)
{
    Console.WriteLine("Банкомат богдана");

    Console.WriteLine("1. Снять деньги");
    Console.WriteLine("2. Поплнить деньги");
    Console.WriteLine("3. Перевести деньги");
    Console.WriteLine("4. Узнать баланс");
    Console.WriteLine("5. Создать счет");
    Console.WriteLine("6. Вывести все счета");

    var input = Console.ReadLine();

    if (int.TryParse(input, out int choice))
        Console.WriteLine("Выполняется действие");

    switch (choice)
    {
        case 1:
            Console.WriteLine("Укажите счёт");
            var account = Console.ReadLine();

            if (account is not null && accounts.ContainsKey(account))
            {
                var balanceOnAccount = accounts[account];

                Console.WriteLine("Введите сумму");

                var sum = Convert.ToInt32(Console.ReadLine());

                if (balanceOnAccount < sum)
                {
                    Console.WriteLine("Недостаточно средств!");
                }
                else
                {
                    accounts[account] -= sum;
                    Console.WriteLine($"Вы сняли {sum} рублей");
                    Console.WriteLine("Деньги сняты");
                }
            }
            else
            {
                Console.WriteLine("Такого счета не существует!");
            }
            break;
        case 2:
            Console.WriteLine("Пополнить деньги");
            Console.WriteLine("Укажите счёт");
            var acc = Console.ReadLine();
            if (acc is not null && accounts.ContainsKey(acc))
            {
                Console.WriteLine("Внесите купюры");
                var cash = Convert.ToInt32(Console.ReadLine());
                accounts[acc] += cash;
                Console.WriteLine($"Зачислено {cash} рублей ");
            }
            else
            {
                Console.WriteLine("Неверно указан счёт");
            }
            break;
        case 3:
            Console.WriteLine("Перевести деньги");
            Console.WriteLine("Выберите исходный счёт");
            var da = Console.ReadLine();
            if (da is not null && accounts.ContainsKey(da))
            {
                var balanceOnAccount = accounts[da];
                Console.WriteLine("Выберите счёт получателя");
                var receiver = Console.ReadLine();
                if (receiver is not null && accounts.ContainsKey(receiver))
                {
                    Console.WriteLine("Введите сумму для перевода");
                    var sum = Convert.ToInt32(Console.ReadLine());

                    if
                        (balanceOnAccount < sum)
                    {
                        Console.WriteLine("Недостаточно средств!");
                    }
                    else
                    {
                        accounts[da] -= sum;
                        Console.WriteLine($"Вы перевели {sum} рублей");
                        accounts[receiver] += sum;
                        Console.WriteLine("Перевод подтверждён");
                    }
                }
                else
                {
                    Console.WriteLine("Такого счёта не существует");
                }
            }

            break;
        case 4:
            Console.WriteLine("Выберите счёт");
            var pa = Console.ReadLine();
            if (pa is not null && accounts.ContainsKey(pa))
            {
                Console.WriteLine("Узнать баланс");
                var balance = accounts[pa];
                Console.WriteLine($" Баланс равен {balance}");
            }
            else
            {
                Console.WriteLine("Указаный счёт недействителен");
            }
            break;


        case 5:
            Console.WriteLine("Введите логин счета");
            var newAccount = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newAccount))
            {
                Console.WriteLine("Введите пароль");
                var pw = Convert.ToInt32(Console.ReadLine())
               ;
                while (pw == 1234)
                {
                    Console.WriteLine("Введите другой пароль");
                    pw = Convert.ToInt32(Console.ReadLine());
                }
                accounts.Add(newAccount, 0);
            }
            break;
        case 6:
            Console.WriteLine("Введите пароль");
            var password = Console.ReadLine();

            while (GLOBAL_PASSWORD != password)
            {
                Console.WriteLine("Попробуйте еще раз!");
                password = Console.ReadLine();
            }

            foreach (var currentAccount in accounts)
            {
                Console.WriteLine(currentAccount);
            }

            break;

        default:
            Console.WriteLine("Выбран неверный вариант! Попробуйте снова");
            break;
    }
}