﻿using System;
using System.Collections;
using FinanceIUA.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace FinanceIUA.StepDefinitions
{
    [Binding]
    public class CurrencyExchangeRateInUkraineSteps
    {

        private FinancePage financePage;
        private IWebDriver driver;
        private string averagePurchasingRate;
        private string averageSellingRate;

        [BeforeScenario()]
        public void Setup()
        {
            driver = new FirefoxDriver();
        }

        [AfterScenario()]
        public void TearDown()
        {
            driver.Quit();
        }


        [Given(@"I have opened the Finance page")]
        public void GivenIHaveOpenedTheFinancePage()
        {
            financePage = FinancePage.NavigateTo(driver);
  
        }
        


        [Given(@"I have grabded average ""(.*)"" rate in Average Rate section")]
        public void GivenIHaveGrabdedAverageRateInAverageRateSection(string currency)
        {
            var averageRates = financePage.GetAverageRates(currency);
            Assert.IsNotNull(averageRates);

            averageSellingRate = averageRates["Selling"].ToString();
            averagePurchasingRate = averageRates["Purchasing"].ToString();

            Assert.IsNotNullOrEmpty(averageSellingRate);
            Assert.IsNotNullOrEmpty(averagePurchasingRate);
        }



        [Then(@"I set ""(.*)"" currency in Ukrainian Banks rates section")]
        public void ThenISetCurrencyInUkrainianBanksRatesSection(string currency)
        {
            StringAssert.AreEqualIgnoringCase(currency, financePage.SetCurrensy(currency));
        }



        [When(@"I compare calculated and grabbed values the result should be equal to ""(.*)"" decimal")]
        public void ThenIGrabAllRatesAndCalculatingAverageValue(int decimalDigits)
        {
            Hashtable averageBanksRates = financePage.GetAverageBanksRates(decimalDigits);
            
            StringAssert.AreEqualIgnoringCase(averagePurchasingRate, averageBanksRates["Purchasing"].ToString());
            StringAssert.AreEqualIgnoringCase(averageSellingRate, averageBanksRates["Selling"].ToString());
        }
        
    }
}
