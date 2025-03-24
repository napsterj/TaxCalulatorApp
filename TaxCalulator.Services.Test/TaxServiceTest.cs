using Moq;
using TaxCalculator.Repo.Interface;
using TaxCalulator.Entities.Entities;
using TaxCalulator.Service.Implementation;

namespace TaxCalulator.Services.Test
{    
    public class TaxServiceTest
    {
        private readonly TaxService _sut;
        private readonly Mock<ITaxRepository> _taxRepository = new();

        public TaxServiceTest()
        {
            _sut = new TaxService(_taxRepository.Object);
        }

        [Fact]
        public void GetVatAndGrossValues_When_Net_Amount_VAT_Rate_Is_Supplied()
        {
            //Arrange
            var price = new Price
            {
                CountryName = "Austria",
                NetPrice = 2.00M,
                VatRate = 13
            };
            decimal vatAmount = 0.26M;
            decimal grossAmount = 2.26M;
            _taxRepository.Setup(x => x.GetVatAndGrossValues(price.NetPrice.Value, price.VatRate))
                          .Returns((vatAmount, grossAmount));

            //Act
            var (actualVatAmount, actualGrossAmount) = _sut.GetVatAndGrossValues(price);

            //Assert
            Assert.Equal(vatAmount, actualVatAmount);
            Assert.Equal(grossAmount, actualGrossAmount);
        }

        [Fact]
        public void GetNetAndVatValues_When_Gross_Amount_VAT_Rate_Is_Supplied()
        {
            //Arrange
            var price = new Price
            {
                CountryName = "UK",
                GrossPrice = 13.00M,
                VatRate = 20.00M
            };

            decimal vatAmount = 2.17M;
            decimal netAmount = 10.83M;

            _taxRepository.Setup(x => x.GetNetAndVatValues(price.GrossPrice.Value, price.VatRate))
                          .Returns((netAmount, vatAmount));

            //Act
            var (actualNetAmount, actualVatAmount) = _sut.GetNetAndVatValues(price);

            //Assert
            Assert.Equal(vatAmount, actualVatAmount);
            Assert.Equal(netAmount, actualNetAmount);
        }

        [Fact]
        public void GetNetAndGrossValues_When_Vat_Amount_VAT_Rate_Is_Supplied()
        {
            //Arrange
            var price = new Price
            {
                CountryName = "Austria",
                VatAmount = 16.00M,
                VatRate = 10.00M
            };

            decimal netAmount = 160.00M;
            decimal grossAmount = 176.00M;

            _taxRepository.Setup(x => x.GetNetAndGrossValues(price.VatAmount.Value, price.VatRate))
                          .Returns((netAmount, grossAmount));

            //Act
            var (actualNetAmount, actualGrossAmount) = _sut.GetNetAndGrossValues(price);

            //Assert
            Assert.Equal(grossAmount, actualGrossAmount);
            Assert.Equal(netAmount, actualNetAmount);
        }        
    }
}
