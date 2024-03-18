using BrValidators;
using System;
using System.Collections.Generic;

namespace ExternalEntities.Infraestructure
{
    public class Configuration
    {
        public Configuration(bool shouldSearchOnQuod, bool shouldSearchBacen, List<string> testCpf)
        {
            ShouldSearchOnQuod = shouldSearchOnQuod;
            ShouldSearchBacen = shouldSearchBacen;

            List<string> cpfs = new List<string>();
            if (testCpf is not null)
                foreach (var cpf in testCpf)
                    cpfs.Add(CPFValidator.Trim(cpf));

            TestCpf = cpfs;
        }

        public bool ShouldSearchOnQuod { get; set; }
        public bool ShouldSearchBacen { get; set; }
        public List<string> TestCpf { get; set; }
        public bool IsTestCpf(string cpf) => TestCpf?.Contains(CPFValidator.Trim(cpf)) ?? false;
        public bool UseQuod(string cpf) => ShouldSearchOnQuod && !IsTestCpf(CPFValidator.Trim(cpf));
        public bool UseBacen(string cpf) => ShouldSearchBacen && !IsTestCpf(CPFValidator.Trim(cpf));
    }
}
