using System;
using Interface.Entities;

namespace Interface.Services
{
    class RentalService //Essa solução sem interface possui algumas deficiências pelo fato de que o serviço de aluguel(RentalService) possuir uma dependência com o serviço de imposto(BrazilTaxService), e acaba ficando fortemente acoplado com o serviço de imposto
    {
        public double PricePerHour { get; private set; }
        public double PricePerDay { get; private set; }

        private BrazilTaxService brazilTaxService = new BrazilTaxService(); //Declaração e instanciação do serviço de imposto (Depêndencia), causando assim uma forte dependência entre os dois serviços pois o RentalService fica dependendo apenas do BrazilTaxService
        //Se posteriormente necessitar de um serviço de imposto de outro país isso causaria dois pontos de alteração, pois será necessário criar uma nova classe para o novo serviço e ainda abrir essa classe e alterar o tipo acima para o novo serviço e isso não é desejável em termos de manutenção.
        //Dessa maneira a classe RentalService não esta fechada para alteração e o ideal é possuir a classe fechada para alteração sendo que se alterar a dependência dela não será necessário alterar nada nessa classe e isso é possível por meio de interfaces(ver a solução com Interface).

        public RentalService(double pricePerHour, double pricePerDay)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
        }

        public void ProcessInvoice(CarRental carRental)
        {
            TimeSpan duration = carRental.Finish.Subtract(carRental.Start);

            double basicPayment = 0.0;

            if(duration.TotalHours <= 12.0)
            {
                basicPayment += PricePerHour * Math.Ceiling(duration.TotalHours);
            }
            else
            {
                basicPayment = PricePerDay * Math.Ceiling(duration.TotalDays);
            }

            double tax = brazilTaxService.Tax(basicPayment);

            carRental.Invoice = new Invoice(basicPayment, tax);
        }

    }
}
