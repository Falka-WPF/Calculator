using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{

    class MathHelper
    {
        
        public MathHelper()
        {
        }
        public double AddNumbers(double x, double y)
        {
            return (x + y);
        }
        public double SubstractNumbers(double x, double y)
        {
            return x - y;
        }

        public double DivideNumbers(double x, double y)
        {
            if (y == 0)
                throw new DivideByZeroException();
            else
                return x / y;
        }
        public double MultiplyNumbers(double x, double y)
        {
            return x * y;
        }

       
        public double MakeOperation(double x, double y, char operation)
        {
            try
            {
                double result = 0;
                if (operation == '+')
                    result = AddNumbers(x, y);
                if (operation == '-')
                    result = SubstractNumbers(x, y);
                if (operation == '/')
                    result = DivideNumbers(x, y);
                if (operation == '*')
                    result = MultiplyNumbers(x, y);
                return result;
            }
            catch
            {
                throw new DivideByZeroException();
            }
        }

    }
}
