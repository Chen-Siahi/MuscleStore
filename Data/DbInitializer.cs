using MuscleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuscleStore.Data
{
    public static class DbInitializer
    {
        //section changed in program.cs in order to extract MuscleStoreContext and initalize!


        public static void Initialize(MuscleStoreContext _context)
        {
            //this will delete existing database
            //_context.Database.EnsureDeleted();

            //if no Admin account already created, lets make one!
            if (!(_context.Branch.Any()))
            {
                Branch city1 = new Branch()
                {
                    City= "Rishon LeTsiyon"
                };
                Branch city2 = new Branch()
                {
                    City = "Tel-Aviv"
                };
                Branch city3 = new Branch()
                {
                    City = "Kfar-Sava"
                };
                _context.Branch.Add(city1);
                _context.Branch.Add(city2);
                _context.Branch.Add(city3);
                _context.SaveChanges();
            }

            if (!(_context.Account.Any(i => i.Type == UserType.Admin)))
            {
                Account KenAdams = new Account()
                {
                    Username = "Admin",
                    FirstName = "ken",
                    LastName = "Adams",
                    Email = "KenAdams@MuscleStore.com",
                    Password = "KenAdams",
                    Type = UserType.Admin,
                    RegistrationDate = DateTime.Now
                };

                _context.Account.Add(KenAdams);
                _context.SaveChanges();
            }



            if (!(_context.Brand.Any()))
            {
                Brand Gasp = new Brand()
                {
                    Name = "Gasp",
                    Description = "GASP products are not made for everybody at the gym – It’s only for the most dedicated athletes!",
                    ImageUrl = "/images/gasp/logo.jpg"
                };
                _context.Brand.Add(Gasp);

                Brand Nike = new Brand()
                {
                    Name = "Nike",
                    Description = "Just Do It.",
                    ImageUrl = "/images/nike/logo.jpg"
                };
                _context.Brand.Add(Nike);

                _context.SaveChanges();
            }



            if (!(_context.Category.Any()))
            {
                Category category1 = new Category() { Name = "Hoodies & Long-Sleeves" };
                 _context.Category.Add(category1);

                Category category2 = new Category() { Name = "Joggers" };
                _context.Category.Add(category2);

                Category category3 = new Category() { Name = "Pants" };
                _context.Category.Add(category3);

                Category category4 = new Category() { Name = "T-Shirts" };
                _context.Category.Add(category4);

                Category category5 = new Category() { Name = "Trainers" };
                _context.Category.Add(category5);
                _context.SaveChanges();
            }


            if (!(_context.Product.Any()))
            {
                Product hoodie1 = new Product()
                {
                    Name = "Gasp ORIGINAL HOODIE",
                    Price = 350,
                    Description = "A grab and go hoodie designed for your life both inside and out of the gym. " +
                                  "Designed in such a way where the hoodie sits perfect in all the right places.",
                    ImageUrl = "/images/gasp/hoodie/index.png",
                    FrontImageUrl = "/images/gasp/hoodie/front.png",
                    BackImageUrl = "/images/gasp/hoodie/back.png",
                    InStock = true,
                    Brand= _context.Brand.FirstOrDefault(b=>b.Name=="Gasp"),
                    Category = _context.Category.FirstOrDefault(c => c.Name == "Hoodies & Long-Sleeves"),
                };
                _context.Product.Add(hoodie1);

                Product hoodie2 = new Product()
                {
                    Name = "1.2 Ibs hoodie",
                    Price = 700,
                    Description = "This hoodie is, as you can tell from its name, heavy! It’s made out of " +
                                   "American fleece in 80% cotton and 20% polyester and has a weight of 520 grams/sqm!",
                    ImageUrl = "/images/gasp/lbsHoodieBlack/index.png",
                    FrontImageUrl = "/images/gasp/lbsHoodieBlack/front.png",
                    BackImageUrl = "/images/gasp/lbsHoodieBlack/back.png",
                    InStock = true,
                    Brand = _context.Brand.FirstOrDefault(b => b.Name == "Gasp"),
                    Category = _context.Category.FirstOrDefault(c => c.Name == "Hoodies & Long-Sleeves"),
                };
                _context.Product.Add(hoodie2);

                Product cargoPants = new Product()
                {
                    Name = "Ops edition cargos",
                    Price = 630,
                    Description = "The comfort and versatility of these pants make them perfect for not " +
                                   "only tactical training but any situation where a durable and functional " +
                                   "pair of pants that is designed for the GASP physique is needed.",
                    ImageUrl = "/images/gasp/cargoBlack/index.png",
                    FrontImageUrl = "/images/gasp/cargoBlack/front.png",
                    BackImageUrl = "/images/gasp/cargoBlack/back.png",
                    InStock = true,
                    Brand = _context.Brand.FirstOrDefault(b => b.Name == "Gasp"),
                    Category = _context.Category.FirstOrDefault(c => c.Name == "Pants"),
                };
                _context.Product.Add(cargoPants);

                Product joggers1 = new Product()
                {
                    Name = "No 89 mesh pant",
                    Price = 630,
                    Description = "Our highly appreciated NO 89 Mesh pants is equipped with huge " +
                                   "logos on the front and back. They are produced in our flexible mesh " +
                                   "fabric and features front pockets with zippers and adjustable drawstrings at the waist.",
                    ImageUrl = "/images/gasp/no90mesh/index.png",
                    FrontImageUrl = "/images/gasp/no90mesh/front.png",
                    BackImageUrl = "/images/gasp/no90mesh/back.png",
                    InStock = true,
                    Brand = _context.Brand.FirstOrDefault(b => b.Name == "Gasp"),
                    Category = _context.Category.FirstOrDefault(c => c.Name == "Joggers"),
                };
                _context.Product.Add(joggers1);

                Product opsShirt = new Product()
                {
                    Name = "Ops edition tee",
                    Price = 200,
                    Description = "The Ops Edition Tee is made out of 100% polyester with hydropolic " +
                   "finish which is profiled to favor the escape of moisture.",
                    ImageUrl = "/images/gasp/OpsShirt/index.png",
                    FrontImageUrl = "/images/gasp/OpsShirt/front.png",
                    BackImageUrl = "/images/gasp/OpsShirt/back.png",
                    InStock = true,
                    Brand = _context.Brand.FirstOrDefault(b => b.Name == "Gasp"),
                    Category = _context.Category.FirstOrDefault(c => c.Name == "T-Shirts"),
                };
                _context.Product.Add(opsShirt);

                Product airForce = new Product()
                {
                    Name = "Nike Air Force 1 High '07",
                    Price = 620,
                    Description = "The legend lives on in the Nike Air Force 1 High '07, " +
                                  "an update on the iconic AF-1 that offers classic court style and premium cushioning.",
                    ImageUrl = "/images/nike/airForce/index.jpg",
                    FrontImageUrl = "/images/nike/airForce/front.jpg",
                    BackImageUrl = "/images/nike/airForce/back.jpg",
                    InStock = true,
                    Brand = _context.Brand.FirstOrDefault(b => b.Name == "Nike"),
                    Category = _context.Category.FirstOrDefault(c => c.Name == "Trainers"),
                };
                _context.Product.Add(airForce);

                _context.SaveChanges();
            }

        }
    }
}


