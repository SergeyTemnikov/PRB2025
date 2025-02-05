using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prb_session2_first_try.Models
{
    public partial class WorkerPrivateInfo : IDataErrorInfo
    {
        public string BirthdayString { get
            {
                if(Birthday == null)
                {
                    return "";
                }
                else
                {
                    return ((DateTime)Birthday).ToString("d");
                }
            }
            set
            {
                if (value == null)
                {
                    Birthday = null;
                }
                else
                {
                    Birthday = Convert.ToDateTime(value);
                }
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case "PrivatePhoneNumber":
                        string phonePattern = @"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$";
                        if (string.IsNullOrWhiteSpace(PrivatePhoneNumber))
                        {
                            break;
                        }
                        else if (!Regex.IsMatch(PrivatePhoneNumber, phonePattern))
                        {
                            error = "Личный номер телефона должен иметь формат +7 (XXX) XXX-XX-XX.";
                        }
                        break;

                    case "BirthdayString":
                        string emailPattern = @"^\d{2}.\d{2}.\d{4}$";
                        if (string.IsNullOrWhiteSpace(BirthdayString))
                        {
                            break;
                        }
                        else if (!Regex.IsMatch(BirthdayString, emailPattern))
                        {
                            error = "Дата рождения должна иметь корректный вид (например, 01.01.1999).";
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
