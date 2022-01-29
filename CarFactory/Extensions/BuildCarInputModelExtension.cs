using CarFactory.InputModels;
using CarFactory_Domain;
using CarFactory_Domain.Models.InputModels;
using CarFactory_Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CarFactory.Extensions
{
    public static class BuildCarInputModelExtension
    {
        public static IEnumerable<CarSpecification> ToCarSpecification(this BuildCarInputModel carsSpecs)
        {
            //Check and transform specifications to domain objects
            List<CarSpecification> wantedCars = new List<CarSpecification>();
            CarSpecification wantedCar = null;
            foreach (BuildCarInputModelItem spec in carsSpecs.Cars)
            {
                PaintJob paint = null;
                Color baseColor = Color.FromName(spec.Specification.Paint.BaseColor);
                switch (spec.Specification.Paint.Type)
                {
                    case Enum.PaintType.Single:
                        paint = new SingleColorPaintJob(baseColor);
                        break;
                    case Enum.PaintType.Stripe:
                        paint = new StripedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.StripeColor));
                        break;
                    case Enum.PaintType.Dot:
                        paint = new DottedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.DotColor));
                        break;
                    default:
                        throw new ArgumentException(string.Format("Unknown paint type %", spec.Specification.Paint.Type));
                }

                IEnumerable<CarSpecification.SpeakerSpecification> frontWindowSpeakers = spec.Specification.FrontWindowSpeakers.ConvertSpeakers();
                IEnumerable<CarSpecification.SpeakerSpecification> doorSpeakers = spec.Specification.DoorSpeakers.ConvertSpeakers();

                wantedCar = new CarSpecification(paint, spec.Specification.Manufacturer, doorSpeakers, frontWindowSpeakers);

                //Whith this way, I can insert the same instance multiple times
                for (int i = 0; i < spec.Amount; i++)
                {
                    wantedCars.Add(wantedCar);
                }
            }
            return wantedCars;
        }


        private static IEnumerable<CarSpecification.SpeakerSpecification> ConvertSpeakers(this IEnumerable<SpeakerSpecificationInputModel> models)
        {
            if(models == null || models.Count() == 0)
            {
                return new List<CarSpecification.SpeakerSpecification>();
            }

            int totalAmount = models.Sum(e => e.Amount);
            if(totalAmount == 0)
            {
                throw new ArgumentException("the attribute 'amount' of speakers should be bigger than 0");
            }

            int counter = 0;
            int incrementedAmount = 0;
            List<CarSpecification.SpeakerSpecification> speakers = new List<CarSpecification.SpeakerSpecification>();
            foreach (SpeakerSpecificationInputModel model in models)
            {
                incrementedAmount += model.Amount;
                while (counter < incrementedAmount)
                {
                    speakers.Add(new CarSpecification.SpeakerSpecification() { IsSubwoofer = model.IsSubwoofer });
                    counter++;
                }
            }
            return speakers;
        }

    }
}
