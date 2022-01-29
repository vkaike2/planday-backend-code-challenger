using CarFactory.Extensions;
using CarFactory.InputModels;
using CarFactory_Assembly;
using CarFactory_Chasis;
using CarFactory_Domain;
using CarFactory_Domain.Models.InputModels;
using CarFactory_Engine;
using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Builders;
using CarFactory_Interior.Interfaces;
using CarFactory_Paint;
using CarFactory_Storage;
using CarFactory_SubContractor;
using CarFactory_Wheels;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mocks;
using static CarFactory_Factory.CarSpecification;

namespace UnitTests
{
    [TestClass]
    public class CarFactoryTests
    {

        [TestMethod]
        public void CarFactory_BuildCars()
        {
            ICarFactory carFactoryMock = this.AssertFirst();

            List<CarSpecification> carsSpecifications = new List<CarSpecification>();
            for (int i = 0; i < 5; i++)
            {
                carsSpecifications.Add(this.MockCarSpecification());
            }
            IEnumerable<Car> cars =  carFactoryMock.BuildCars(carsSpecifications);

            cars.Count().Should().Be(5);
        }


        [TestMethod]
        public void CarFactory_TestCaseSensitivePaint_lowerCase()
        {
            string mockRequestBody = @"
{
    ""cars"": [
        {
            ""amount"": 1,
            ""specification"": {
                ""paint"": {
                    ""type"": ""stripe"",
                    ""baseColor"": ""Blue"",
                    ""stripeColor"": ""Orange"",
                    ""dotColor"": null
                },
                ""manufacturer"": ""PlanfaRomeo"",
                ""frontWindowSpeakers"": [
                    {
                        ""isSubwoofer"": true,
                        ""amount"": 2
                    }
                ],
                ""DoorSpeakers"": [
                    {
                        ""isSubwoofer"": false,
                        ""amount"": 1
                    }
                ]
            }
        }
    ]
}";

            BuildCarInputModel carsSpecs = JsonConvert.DeserializeObject<BuildCarInputModel>(mockRequestBody);
            List<CarSpecification> CarSpecification =  carsSpecs.ToCarSpecification().ToList();
            CarSpecification[0].PaintJob.StripeColor.Should().Be(Color.Orange);
        }

        [TestMethod]
        public void CarFactory_TestCaseSensitivePaint_upperCase()
        {
            string mockRequestBody = @"
{
    ""cars"": [
        {
            ""amount"": 1,
            ""specification"": {
                ""paint"": {
                    ""type"": ""Stripe"",
                    ""baseColor"": ""Blue"",
                    ""stripeColor"": ""Orange"",
                    ""dotColor"": null
                },
                ""manufacturer"": ""PlanfaRomeo"",
                ""frontWindowSpeakers"": [
                    {
                        ""isSubwoofer"": true,
                        ""amount"": 2
                    }
                ],
                ""DoorSpeakers"": [
                    {
                        ""isSubwoofer"": false,
                        ""amount"": 1
                    }
                ]
            }
        }
    ]
}";

            BuildCarInputModel carsSpecs = JsonConvert.DeserializeObject<BuildCarInputModel>(mockRequestBody);
            List<CarSpecification> CarSpecification = carsSpecs.ToCarSpecification().ToList();
            CarSpecification[0].PaintJob.StripeColor.Should().Be(Color.Orange);
        }

        [TestMethod]
        public void CarFactory_TestCaseSensitivePaint_mixedCase()
        {
            string mockRequestBody = @"
{
    ""cars"": [
        {
            ""amount"": 1,
            ""specification"": {
                ""paint"": {
                    ""type"": ""StrIpE"",
                    ""baseColor"": ""Blue"",
                    ""stripeColor"": ""Orange"",
                    ""dotColor"": null
                },
                ""manufacturer"": ""PlanfaRomeo"",
                ""frontWindowSpeakers"": [
                    {
                        ""isSubwoofer"": true,
                        ""amount"": 2
                    }
                ],
                ""DoorSpeakers"": [
                    {
                        ""isSubwoofer"": false,
                        ""amount"": 1
                    }
                ]
            }
        }
    ]
}";

            BuildCarInputModel carsSpecs = JsonConvert.DeserializeObject<BuildCarInputModel>(mockRequestBody);
            List<CarSpecification> CarSpecification = carsSpecs.ToCarSpecification().ToList();
            CarSpecification[0].PaintJob.StripeColor.Should().Be(Color.Orange);
        }

        private CarSpecification MockCarSpecification()
        {
            List<SpeakerSpecification> speakerSpecifications = new List<SpeakerSpecification>()
            {
                new SpeakerSpecification(){IsSubwoofer = true},
                new SpeakerSpecification(){IsSubwoofer = true}
            };

            return new CarSpecification(
                new StripedPaintJob(Color.Aqua, Color.Black),
                Manufacturer.Plandrover,
                speakerSpecifications,
                speakerSpecifications);
        }

        private ICarFactory AssertFirst()
        {
            IStorageProvider storageProviderMock = new StorageProviderMock();
            IGetChassisRecipeQuery getChassisRecipeQueryMock = new GetChassisRecipeQuery(storageProviderMock);
            IGetEngineSpecificationQuery getEngineSpecificationMock = new GetEngineSpecificationQuery(storageProviderMock);
            IGetRubberQuery rubberyQueryMock = new GetRubberQuery(storageProviderMock);

            ISteelSubcontractor steelSubcontractorMock = new SteelSubcontractor();
            IGetPistons getPistonsMock = new GetPistons();

            IDashboardBuilder dashboardBuilderMock = new DashboardBuilder();
            ISeatBuilder seatBuilderMock = new SeatBuilder();
            ISpeakerBuilder speakerBuilderMock = new SpeakerBuilder();

            IChassisProvider chassisProviderMock = new ChassisProvider(steelSubcontractorMock, getChassisRecipeQueryMock, null);
            IEngineProvider enginerProviderMock = new EngineProvider(getPistonsMock, steelSubcontractorMock, getEngineSpecificationMock, null);
            IPainter painterMock = new Painter();
            IInteriorProvider interiorProviderMock = new InteriorProvider(dashboardBuilderMock, seatBuilderMock, speakerBuilderMock);
            IWheelProvider wheelProviderMock = new WheelProvider(rubberyQueryMock);
            ICarAssembler carAssemblerMock = new CarAssembler();


            return new CarFactory_Factory.CarFactory(
                chassisProviderMock,
                enginerProviderMock,
                painterMock,
                interiorProviderMock,
                wheelProviderMock,
                carAssemblerMock);
        }

    }
}
