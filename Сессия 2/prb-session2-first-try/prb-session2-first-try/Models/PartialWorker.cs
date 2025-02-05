using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prb_session2_first_try.Models
{
    public partial class Worker : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case "FullName":
                        if (string.IsNullOrWhiteSpace(FullName))
                        {
                            error = "Имя не должно быть пустым.";
                        }
                        else if (FullName.Split(' ').Length != 3)
                        {
                            error = "Имя должно быть полным (Фамилия имя отчество).";
                        }
                        break;

                    case "WorkPhoneNumber":
                        string phonePattern = @"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$";
                        if (string.IsNullOrWhiteSpace(WorkPhoneNumber))
                        {
                            error = "Рабочий номер телефона не должен быть пустым.";
                        }
                        else if (!Regex.IsMatch(WorkPhoneNumber, phonePattern))
                        {
                            error = "Рабочий номер телефона должен иметь формат +7 (XXX) XXX-XX-XX.";
                        }
                        break;

                    case "Email":
                        string emailPattern = @"^\w+@\w+.\w{2,}$";
                        if (string.IsNullOrWhiteSpace(Email))
                        {
                            error = "Почта не должна быть пустой.";
                        }
                        else if (!Regex.IsMatch(Email, emailPattern))
                        {
                            error = "Почта должна иметь корректный вид (например, example@example.com или кириллица@домен.рф).";
                        }
                        break;
                }

                return error;
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
