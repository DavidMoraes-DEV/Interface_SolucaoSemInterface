using Interface.Entities;
using Interface.Services;
using System;
using System.Globalization;

namespace Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            * INTERFACE:
            - Interface é um tipo que define um conjunto de operações que uma classe(ou struct) deve IMPLEMENTAR.
            - A interface estabele um CONTRATO  que a classe (ou struct) deve cumprir.
            - Convenção: Utilizar o "I" no inicio do nome da classe para identifica-la como interface.
            
            * PRA QUE INTERFACE?
            - Para criar sistemas com baixo acoplamento e flexíveis.    
            
            * PROBLEMA EXEMPLO: (SEM INTERFACE)
            - Uma locadora brasileira de carros cobra um valor por hora para locações de até 12 horas. Porém, se a duração da locação ultrapassar 12 horas,
            a locação será cobrada com base em um valor diário. Além do valor da locação, é acrescido no preço o valor do imposto conforme regras do país
            que, no caso do Brasil, é 20% para valores até 100,00, ou 15% para valores acima de 100.00. Fazer um programa que lê os dados da locação
            (modelo do carro, intante inicial e final da locação), bem como o valor por hora e o valor diário de locação, valor do imposto e valor total do
            pagamento (contendo valor da locação, valor do imposto e valor total do pagamento) e informar os dados na tela. Veja os exemplos:

            EXEMPLO 1:
            Enter rental data
            Car model: Civic
            Pickup (dd/MM/yyyy hh:MM): 25/06/2018 10:30
            Return (dd/MM/yyyy hh:MM): 25/06/2018 14:40
            Enter price per hour: 10.00
            Enter price per day: 130.00
            INVOICE:
            Basic payment: 50.00
            Tax: 10.00
            Total payment: 60.00
             */

            //SOLUÇÃO SEM O USO DE INTERFACE: Observar o código da Classe RentalService que possui uma dependência fortemente acoplada com o BrazilTaxService
            Console.WriteLine("Enter rental data");
            Console.Write("Car model: ");
            string model = Console.ReadLine();
            Console.Write("Pickup (dd/MM/yyyy hh:MM): ");
            DateTime pickup = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            Console.Write("Rentur (dd/MM/yyyy hh:MM): ");
            DateTime _return = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            Console.Write("Enter price per hour: ");
            double pricePerHour = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.Write("Enter price per day: ");
            double pricePerDay = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            CarRental carRental = new CarRental(pickup, _return, new Vehicle(model));

            RentalService rentalService = new RentalService(pricePerHour, pricePerDay);

            rentalService.ProcessInvoice(carRental);

            Console.WriteLine("INVOICE:");
            Console.WriteLine(carRental.Invoice);
        }
    }
}
