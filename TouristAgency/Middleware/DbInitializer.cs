using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class DbInitializer
    {
        public static void Initialize(AgencyContext db)
        {
            db.Database.EnsureCreated();
            
            if (db.Hotels.Any())
            {
                return;   // База данных инициализирована
            }

            int clients_number = 300;
            int hotels_number = 300;
            int employees_number = 300;
            int rests_number = 300;
            int service_number = 300;
            int servicelist_number = 300;
            int vouchers_number = 35;
            string fullname;
            string position;
            string voc = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫБЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыь";
            int age;
            int telephone;

            Random randObj = new Random(1);
            for (int empID = 1; empID <= employees_number; empID++)
            {
                //создаем объект Random, генерирующий случайные числа

                fullname = GenRandomString(voc, 10);
                position = GenRandomString(voc, 10);
                age = randObj.Next(18, 60);
                telephone = randObj.Next(10000, 99999);
                db.Employees.Add(new Employee { Fullname = fullname, Position = position, Age = age, Telephone = telephone });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();

            for (int restID = 1; restID <= rests_number; restID++)
            {
                fullname = GenRandomString(voc, 10);
                string description = GenRandomString(voc, 10);
                string constraints = GenRandomString(voc, 10);
                db.TypeRests.Add(new TypeRest
                {
                    Name = fullname,
                    Description = description,
                    Constraints = constraints
                });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();

            //Заполнение таблицы показаний
            for (int clientID = 1; clientID <= clients_number; clientID++)
            {
                fullname = GenRandomString(voc, 10);
                DateTime birthdate = DateTime.Now.Date;
                string sex = GenRandomString(voc, 1);
                string address = GenRandomString(voc, 10);
                telephone = randObj.Next(10000, 99999);
                string passdata = GenRandomString(voc, 10);
                int discount = randObj.Next(1, 100);
                db.Clients.Add(new Client
                {
                    FullName = fullname,
                    DateBirth = birthdate,
                    Sex = sex,
                    Adress = address,
                    Telephone = telephone,
                    PassData = passdata,
                    Discount = discount
                });
            }
            db.SaveChanges();

            for (int hotelID = 1; hotelID <= hotels_number; hotelID++)
            {
                fullname = GenRandomString(voc, 10);
                string country = GenRandomString(voc, 10);
                string city = GenRandomString(voc, 10);
                string address = GenRandomString(voc, 10);
                telephone = randObj.Next(10000, 99999); 
                int stars = randObj.Next(1, 10);
                string contactface = GenRandomString(voc, 10);
                string fotohotel = GenRandomString(voc, 10);
                db.Hotels.Add(new Hotel
                {
                    Name = fullname,
                    Country = country,
                    City = city,
                    Adress = address,
                    Telephone = telephone,
                    Stars = stars,
                    ContactFace = contactface,
                    FotoHotel = fotohotel
                });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();

            for (int serviceID = 1; serviceID <= hotels_number; serviceID++)
            {
                fullname = GenRandomString(voc, 10);
                string description = GenRandomString(voc, 10);
                int price = randObj.Next(100, 1000000);
                db._Services.Add(new Service
                {
                    Name = fullname,
                    Description = description,
                    Price = price
                });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();

            for (int voucherID = 1; voucherID <= vouchers_number; voucherID++)
            {
                DateTime datebeginning = DateTime.Now.Date;
                DateTime expirationdate = datebeginning.AddDays(-voucherID);
                int hotelid = randObj.Next(1, hotels_number - 1);
                int typerestid = randObj.Next(1, rests_number - 1);
                int clientid = randObj.Next(1, clients_number - 1);
                int employeeid = randObj.Next(1, employees_number - 1);
                bool bookingnote;
                bool paymentnote;
                if (voucherID / 2 == 0)
                    bookingnote = true;
                else
                    bookingnote = false;
                if (voucherID / 3 == 0)
                    paymentnote = true;
                else
                    paymentnote = false;

                db.Vouchers.Add(new Voucher
                {
                    DateBeginning = datebeginning,
                    ExpirationDate = expirationdate,
                    HotelID = hotelid,
                    TypeRestID = typerestid,
                    ClientID = clientid,
                    EmployeeID = employeeid,
                    BookingNote = bookingnote,
                    PaymentNote = paymentnote
                });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();

            for (int servicelistID = 1; servicelistID <= servicelist_number; servicelistID++)
            {
                int serviceid = randObj.Next(1, service_number - 1);
                int voucherid = randObj.Next(1, vouchers_number - 1);
                db.ServiceList.Add(new ServiceList
                {
                    ServiceID = serviceid,
                    VoucherID = voucherid
                });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();
        }

        static string GenRandomString(string Alphabet, int Length)
        {
            Random rnd = new Random();
            //объект StringBuilder с заранее заданным размером буфера под результирующую строку
            StringBuilder sb = new StringBuilder(Length - 1);
            //переменную для хранения случайной позиции символа из строки Alphabet
            int Position = 0;
            string ret = "";
            for (int i = 0; i < Length; i++)
            {
                //получаем случайное число от 0 до последнего
                //символа в строке Alphabet
                Position = rnd.Next(0, Alphabet.Length - 1);
                //добавляем выбранный символ в объект
                //StringBuilder
                ret = ret + Alphabet[Position];
            }
            return ret;
        }
    }
}
