using CarFactory_Domain;
using CarFactory_Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public class ChassisWelder
    {
        private ChassisBack _firstPart;
        private ChassisCabin _secondPart;
        private ChassisFront _thirdPart;

        public Chassis Weld(ChassisBack _chassisBack,
                            ChassisCabin chassisCabin,
                            ChassisFront chassisFront)
        {
            this.CleanWelder();

            this.StartWeld(_chassisBack);
            this.ContinueWeld(chassisCabin);
            this.FinishWeld(chassisFront);

            return GetChassis();
        }

        private void CleanWelder()
        {
            _firstPart = null;
            _secondPart = null;
            _thirdPart = null;
        }

        private void StartWeld(ChassisBack firstPart)
        {
            if (_firstPart != null) return;

            _firstPart = firstPart;
        }

        private void ContinueWeld(ChassisCabin secondPart)
        {
            if (_secondPart != null) return;

            _secondPart = secondPart;
        }

        private void FinishWeld(ChassisFront thirdPart)
        {
            if (_thirdPart != null) return;
                _thirdPart = thirdPart;
        }

        private Chassis GetChassis()
        {
            if (_firstPart == null || _secondPart == null || _thirdPart == null)
            {
                throw new CarFactoryException("Chassis not finished");
            }
            var isValid = false;

            if (_firstPart.GetType() == "ChassisBack" && _secondPart.GetType() == "ChassisCabin" && _thirdPart.GetType() == "ChassisFront")
            {
                isValid = true;
            }

            string back = _firstPart.GetChassisType();
            string cabin = _secondPart.GetChassisType();
            string front = _thirdPart.GetChassisType();

            string description = _secondPart.GetChassisType() + " " + _firstPart.GetChassisType() + " " + _thirdPart.GetChassisType();
            return new Chassis(description, isValid);
        }
    }
}
