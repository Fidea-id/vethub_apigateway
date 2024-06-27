using Domain.Entities;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Domain.Utils
{
    public static class FormatUtil
    {
        public static int CalculateDaysBetween(DateTime start, DateTime end)
        {
            // Ensure that the end date is later than the start date
            if (end < start)
            {
                throw new ArgumentException("End date must be later than start date");
            }

            // Calculate the number of full days between the start and end dates
            int daysDifference = (end.Date - start.Date).Days;

            // Add one to the count if there is any time difference beyond the start day
            if (end.TimeOfDay > start.TimeOfDay)
            {
                daysDifference++;
            }

            // Ensure at least one day is counted
            return Math.Max(daysDifference, 1);
        }
        public static string MethodTypeString(MethodType type)
        {
            var result = "undefined";
            if (type == MethodType.Create)
            {
                result = "Create";
            }
            else if (type == MethodType.Delete)
            {
                result = "Delete";
            }
            else
            {
                result = "Update";
            }
            return result;
        }
        public static double CountPercentageMonth(int month, int all)
        {
            if (all == 0)
            {
                return 0;
                //throw new InvalidOperationException("CountAllData cannot be zero to calculate percentage increase.");
            }

            double increasePercentage = ((double)month / all) * 100;
            return increasePercentage;
        }
        public static double CountDoublePercentageMonth(double month, double all)
        {
            if (all == 0)
            {
                return 0;
                //throw new InvalidOperationException("CountAllData cannot be zero to calculate percentage increase.");
            }

            double increasePercentage = ((double)month / all) * 100;
            return increasePercentage;
        }
        public static string GenerateOrdersNumber(string latest = null)
        {
            DateTime currentDate = DateTime.Now;
            string year = currentDate.Year.ToString().Substring(2);
            string month = currentDate.Month.ToString("D2");

            int latestYear = 0;
            int latestMonth = 0;
            int latestNumber = 0;

            if (latest != null && latest.Length == 11) // Check if the latest code has the correct format
            {
                int.TryParse("20" + latest.Substring(3, 2), out latestYear);
                int.TryParse(latest.Substring(5, 2), out latestMonth);
                int.TryParse(latest.Substring(latest.Length - 4), out latestNumber);
            }

            if (latestYear != currentDate.Year || latestMonth != currentDate.Month)
            {
                latestNumber = 0; // Reset the number if not in the current month and year
            }
            latestNumber++; // Increment the latest number

            string recordNumber = latestNumber.ToString("D4");
            string orderNumber = $"OR_{year}{month}{recordNumber}";
            return orderNumber;
        }
        public static string GenerateMedicalRecordCode(string latest = null)
        {
            DateTime currentDate = DateTime.Now;
            string year = currentDate.Year.ToString().Substring(2);
            string month = currentDate.Month.ToString("D2");

            int latestYear = 0;
            int latestMonth = 0;
            int latestNumber = 0;

            if (latest != null && latest.Length == 11) // Check if the latest code has the correct format
            {
                int.TryParse("20" + latest.Substring(3, 2), out latestYear);
                int.TryParse(latest.Substring(5, 2), out latestMonth);
                int.TryParse(latest.Substring(latest.Length - 4), out latestNumber);
            }

            if (latestYear != currentDate.Year || latestMonth != currentDate.Month)
            {
                latestNumber = 0; // Reset the number if not in the current month and year
            }
            latestNumber++; // Increment the latest number

            string recordNumber = latestNumber.ToString("D4");
            string orderNumber = $"MR_{year}{month}{recordNumber}";
            return orderNumber;
        }
        public static string GetAgeInfo(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;
            int years = currentDate.Year - dateOfBirth.Year;
            int months = currentDate.Month - dateOfBirth.Month;

            if (currentDate.Day < dateOfBirth.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            return $"{years} years and {months} months";
        }
        public static string GetTimeAgo(DateTime checkDate)
        {
            TimeSpan timeDifference = DateTime.Now - checkDate;

            if (timeDifference.TotalMinutes < 1)
            {
                return "Just Now";
            }
            else if (timeDifference.TotalMinutes < 60)
            {
                int minutes = (int)timeDifference.TotalMinutes;
                return $"{minutes} {(minutes == 1 ? "Minute" : "Minutes")} Ago";
            }
            else if (timeDifference.TotalHours < 24)
            {
                int hours = (int)timeDifference.TotalHours;
                return $"{hours} {(hours == 1 ? "Hour" : "Hours")} Ago";
            }
            else if (timeDifference.TotalDays < 30)
            {
                int days = (int)timeDifference.TotalDays;
                return $"{days} {(days == 1 ? "Day" : "Days")} Ago";
            }
            else if (timeDifference.TotalDays < 365)
            {
                int months = (int)(timeDifference.TotalDays / 30);
                return $"{months} {(months == 1 ? "Month" : "Months")} Ago";
            }
            else
            {
                int years = (int)(timeDifference.TotalDays / 365);
                return $"{years} {(years == 1 ? "Year" : "Years")} Ago";
            }
        }
        public static int GetIdEntity(object entity)
        {
            var sourceProperties = entity.GetType().GetProperties();
            var entityProperty = sourceProperties.FirstOrDefault(x => x.Name == "Id");
            if (entityProperty != null)
            {
                var value = entityProperty.GetValue(entity);
                if (value != null)
                {
                    return (int)value;
                }
            }
            return 0;
        }

        public static string GenerateUniqueFileName(string fileName)
        {
            // Get the file extension
            string fileExtension = Path.GetExtension(fileName);

            // Generate a random string
            string uniqueFilename = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{fileExtension}";

            // Return the file name with the random string and the original file extension
            return uniqueFilename;
        }

        public static void TrimObjectProperties(object obj)
        {
            if (obj == null) return;

            var type = obj.GetType();
            var properties = type.GetProperties().Where(p => p.PropertyType == typeof(string));
            foreach (var property in properties)
            {
                var value = (string)property.GetValue(obj);
                if (value != null)
                {
                    property.SetValue(obj, value.Trim());
                }
            }
        }
        public static void SetIsActive<T>(T data, bool isActive) where T : BaseEntity
        {
            Type type = typeof(T);
            PropertyInfo lastUpdateByProperty = type.GetProperty("IsActive");
            if (lastUpdateByProperty != null)
            {
                lastUpdateByProperty.SetValue(data, isActive);
            }
        }
        public static void SetDateBaseEntity<T>(T data, bool isUpdate = false) where T : BaseEntity
        {
            data.UpdatedAt = DateTime.Now;
            if (!isUpdate)
            {
                data.CreatedAt = DateTime.Now;
            }
        }
        public static void SetOppositeActive<T>(T data) where T : class
        {
            Type type = typeof(T);
            PropertyInfo lastUpdateByProperty = type.GetProperty("IsActive");
            if (lastUpdateByProperty != null)
            {
                bool currentValue = (bool)lastUpdateByProperty.GetValue(data);
                lastUpdateByProperty.SetValue(data, !currentValue);
            }
        }

        public static void ConvertUpdateObject<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                if (sourceProperty.Name.Contains("Id") && sourceProperty.Name != "Id")
                {
                    // Check if the value is 0 or null and continue if necessary
                    if (sourceProperty.GetValue(source) == null || (int)sourceProperty.GetValue(source) == 0)
                    {
                        continue;
                    }
                }
                if (sourceProperty.Name == "Id")
                {
                    var destinationProperty2 = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                    if (destinationProperty2 != null)
                    {
                        sourceProperty.SetValue(source, destinationProperty2.GetValue(destination));
                    }
                    continue;
                }
                if (sourceProperty.Name == "Longitude" || sourceProperty.Name == "Latitude")
                {
                    // Check if the value is 0 or null and continue if necessary
                    if (sourceProperty.GetValue(source) == null || (double)sourceProperty.GetValue(source) == 0)
                    {
                        continue;
                    }
                }
                if (sourceProperty.Name == "CreatedAt") continue;
                if (sourceProperty.Name == "UpdatedAt") continue;
                if (sourceProperty.GetValue(source) == null) continue;

                var destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                if (destinationProperty != null)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            if (password != string.Empty && password != null)
            {
                if (!Regex.IsMatch(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})")) return false;

                var specialchar = new List<string> { "@", "#", "$", "%", "^", "&", "+", "=", ".", ",", "<", ">", "`", "!", "/", "?", "@", "\"", "'", "~", "\\", "[", "]", "{", "}", "*", "(", ")", "-", "+", "|", "_", "[", "]", "/", ":", ";" };
                if (!specialchar.Any(a => password.Contains(a))) return false;

                return true;
            }
            else return false;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string DeleteSerializeTempData<T>(List<T> temp, T deletedData)
        {
            temp.Remove(deletedData);
            return JsonConvert.SerializeObject(temp);
        }
        public static string InsertSerializeTempData<T>(List<T> temp, T newData)
        {
            if (temp == null)
            {
                var list = new List<T>();
                list.Add(newData);
                return JsonConvert.SerializeObject(list);
            }
            temp.Add(newData);
            return JsonConvert.SerializeObject(temp);
        }
        public static List<T> DeserializeTempData<T>(string data)
        {
            if (string.IsNullOrEmpty(data)) return default(List<T>);
            var list = JsonConvert.DeserializeObject<List<T>>(data);
            return list;
        }

        public static string ReplaceSpacesWithUnderscores(string input)
        {
            return input.Replace(" ", "_");
        }
    }
}
