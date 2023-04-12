using BE.Data.Entities;
using BE.Data.Enums;
using BE.Ultilities.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        public DbInitializer(AppDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                var _item1 = new Role()
                {
                    Name = CommonConstants.AdminRole,
                    Description = "Admin system"
                };
                await _roleManager.CreateAsync(_item1);

                var _item4 = new Role()
                {
                    Name = CommonConstants.ClientRole,
                    Description = "Client"
                };
                await _roleManager.CreateAsync(_item4);
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            };
            if (!_userManager.Users.Any())
            {
                var check_01 = await _userManager.CreateAsync(new User()
                {
                    UserName = "admin",
                    FullName = "namquang dinh",
                    Email = "admin@gmail.com",
                    Avatar = "https://res.cloudinary.com/namqd98/image/upload/v1592814619/Default/defaultuser_qyklmd.png",
                    PhoneNumber = "0348621188",
                    FacebookUrl = "https://www.facebook.com/namquangggg/",
                    TwitterUrl = "",
                    WebsiteUrl = "https://www.linkedin.com/in/namqd98/",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                }, "123456$");

                if (check_01.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("admin"); // tim user admin
                    _userManager.AddToRoleAsync(user, CommonConstants.AdminRole).Wait(); // add admin vao role admin
                }

                var check_02 = await _userManager.CreateAsync(new User()
                {
                    UserName = "client02",
                    FullName = "Đinh Quang Nam",
                    Avatar = "https://res.cloudinary.com/namqd98/image/upload/v1592814619/Default/defaultuser_qyklmd.png",
                    Email = "client02@gmail.com",
                    PhoneNumber = "0348621188",
                    FacebookUrl = "https://www.facebook.com/namquangggg/",
                    TwitterUrl = "",
                    WebsiteUrl = "https://www.linkedin.com/in/namqd98/",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                }, "123456$");

                if (check_02.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("client02");
                    _userManager.AddToRoleAsync(user, CommonConstants.ClientRole).Wait();
                }

                var check_05 = await _userManager.CreateAsync(new User()
                {
                    UserName = "admin05",
                    FullName = "Đinh Quang Nam 05",
                    Avatar = "https://res.cloudinary.com/namqd98/image/upload/v1592814619/Default/defaultuser_qyklmd.png",
                    Email = "admin05@gmail.com",
                    PhoneNumber = "0348621188",
                    FacebookUrl = "https://www.facebook.com/namquangggg/",
                    TwitterUrl = "",
                    WebsiteUrl = "https://www.linkedin.com/in/namqd98/",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                }, "123456$");

                if (check_05.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("admin05");
                    _userManager.AddToRoleAsync(user, CommonConstants.AdminRole).Wait();
                }

                var check_03 = await _userManager.CreateAsync(new User()
                {
                    UserName = "client03",
                    FullName = "Đinh Quang Nam 03",
                    Avatar = "https://res.cloudinary.com/namqd98/image/upload/v1592814619/Default/defaultuser_qyklmd.png",
                    Email = "client3@gmail.com",
                    PhoneNumber = "0348621188",
                    FacebookUrl = "https://www.facebook.com/namquangggg/",
                    TwitterUrl = "",
                    WebsiteUrl = "https://www.linkedin.com/in/namqd98/",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                }, "123456$");

                if (check_03.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("client03");
                    _userManager.AddToRoleAsync(user, CommonConstants.ClientRole).Wait();
                }

                var check_04 = await _userManager.CreateAsync(new User()
                {
                    UserName = "client04",
                    FullName = "Đinh Quang Nam 04",
                    Avatar = "https://res.cloudinary.com/namqd98/image/upload/v1592814619/Default/defaultuser_qyklmd.png",
                    Email = "client04@gmail.com",
                    PhoneNumber = "0348621188",
                    FacebookUrl = "https://www.facebook.com/namquangggg/",
                    TwitterUrl = "",
                    WebsiteUrl = "https://www.linkedin.com/in/namqd98/",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                }, "123456$");

                if (check_04.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("client04");
                    _userManager.AddToRoleAsync(user, CommonConstants.ClientRole).Wait();
                }
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            };

            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                      new Function() { Name = "Manage posts", SortOrder = 1,URL = "/properties",IconCss = "fa-tasks"  },

                      new Function() { Name = "Messages", SortOrder = 2,URL = "/messages",IconCss = "fa-comments"  },

                      new Function() { Name = "Manage users", SortOrder = 6,URL = "/users",IconCss = "fa-users"  },

                      new Function() { Name = "Manage features", SortOrder = 6,URL = "/features",IconCss = "fa-cogs"  },

                      new Function() { Name = "Logging", SortOrder = 6,URL = "/logs",IconCss = "fa-minus-square-o"  },
                });
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            };

            if (_context.PropertyCategoris.Count() == 0)
            {
                _context.PropertyCategoris.AddRange(new List<PropertyCategory>()
                {
                    new PropertyCategory { Name="House", Description="House"},
                    new PropertyCategory { Name="Apartment", Description="Apartment"},
                    new PropertyCategory { Name="Room", Description="Room"},
                });
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            };

            if (_context.AnnouncementTypes.Count() == 0)
            {
                _context.AnnouncementTypes.AddRange(new List<AnnouncementType>()
                {
                    new AnnouncementType { Name="Warning", Icon="report_problem"},
                    new AnnouncementType { Name="Approved", Icon="check_circle"},
                    new AnnouncementType { Name="Deleted", Icon="delete_forever"},
                    new AnnouncementType { Name = "Normal", Icon="notifications_active"}
                });
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            };

            if (_context.RentalTypes.Count() == 0)
            {
                _context.RentalTypes.AddRange(new List<RentalType>()
                {
                    new RentalType() { Name="For rent"},
                    new RentalType() { Name="Need rent"},
                    new RentalType() { Name="For sharing"},
                    new RentalType() { Name="Need sharing"}
                });
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if (_context.Citis.Count() == 0)
            {
                _context.Citis.AddRange(new List<City>()
                {
                    new City() { Name = "Hồ Chí Minh"},
                    new City() { Name = "Hà Nội"},
                    new City() { Name = "Đà Nẵng"},
                    new City() { Name = "Thừa Thiên Huế"},
                    new City() { Name = "Đăk Nông"},
                    new City() { Name = "Buôn Mê Thuật"},
                    new City() { Name = "Bình Dương"},
                    new City() { Name = "Bình Định"},
                    new City() { Name = "Long An"},
                    new City() { Name = "Thanh Hóa"},
                    new City() { Name = "Nghệ An"},
                    new City() { Name = "Cần Thơ"},
                    new City() { Name = "Đồng Nai"},
                    new City() { Name = "Cà Mau"},
                    new City() { Name = "Thái Bình"},
                });
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if (_context.Districts.Count() == 0)
            {
                _context.Districts.AddRange(new List<District>()
                {
                    new District() { CityId = 1, Name = "Quận 1"},
                    new District() { CityId = 1, Name = "Quận 2"},
                    new District() { CityId = 1, Name = "Quận 3"},
                    new District() { CityId = 1, Name = "Quận 4"},
                    new District() { CityId = 1, Name = "Quận 5"},
                    new District() { CityId = 1, Name = "Quận 6"},
                    new District() { CityId = 1, Name = "Quận 7"},
                    new District() { CityId = 1, Name = "Quận 8"},
                    new District() { CityId = 1, Name = "Quận 9"},
                    new District() { CityId = 1, Name = "Quận 10"},
                    new District() { CityId = 1, Name = "Quận 11"},
                    new District() { CityId = 1, Name = "Quận 12"},
                    new District() { CityId = 1, Name = "Quận Bình Thạnh"},
                    new District() { CityId = 1, Name = "Quận Thủ Đức"},
                    new District() { CityId = 1, Name = "Quận Gò Vấp"},
                    new District() { CityId = 1, Name = "Quận Bình Tân"},
                    new District() { CityId = 1, Name = "Quận Tân Bình"},
                    new District() { CityId = 1, Name = "Quận Tân Phú"},
                    new District() { CityId = 1, Name = "Huyện Bình Chánh"},
                    new District() { CityId = 1, Name = "Huyện Nhà Bè"},
                    new District() { CityId = 1, Name = "Huyện Cần Giờ"},
                    new District() { CityId = 1, Name = "Huyện Hóc Môn"},
                    new District() { CityId = 1, Name = "Huyện Củ Chi"},
                });
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if (_context.Wards.Count() == 0)
            {
                _context.Wards.AddRange(new List<Wards>()
                {
                    new Wards() { DistrictId = 1, Name = "Phường Tân Định"},
                    new Wards() { DistrictId = 1, Name = "Phường Đa Kao"},
                    new Wards() { DistrictId = 1, Name = "Phường Bến Nghé"},
                    new Wards() { DistrictId = 1, Name = "Phường Bến Thành"},
                    new Wards() { DistrictId = 1, Name = "Phường Nguyễn Thái Bình"},
                    new Wards() { DistrictId = 1, Name = "Phường Phạm Ngũ Lão"},
                    new Wards() { DistrictId = 1, Name = "Phường Cầu Ông Lãnh"},

                    new Wards() { DistrictId = 2, Name = "Phường An Khánh"},
                    new Wards() { DistrictId = 2, Name = "Phường An Lợi Đông"},
                    new Wards() { DistrictId = 2, Name = "Phường An Phú"},
                    new Wards() { DistrictId = 2, Name = "Phường Binh An"},
                    new Wards() { DistrictId = 2, Name = "Phường Bình Khánh"},
                    new Wards() { DistrictId = 2, Name = "Phường Bình Trưng Đông"},
                    new Wards() { DistrictId = 2, Name = "Phường Bình Trưng Tây"},
                    new Wards() { DistrictId = 2, Name = "Phường Cát Lái"},
                    new Wards() { DistrictId = 2, Name = "Phường Thạnh Mỹ Lợi"},
                    new Wards() { DistrictId = 2, Name = "Phường Thảo Điền"},
                    new Wards() { DistrictId = 2, Name = "Phường Thủ Thiêm"},

                    new Wards() { DistrictId = 3, Name = "Phường 1"},
                    new Wards() { DistrictId = 3, Name = "Phường 2"},
                    new Wards() { DistrictId = 3, Name = "Phường 3"},
                    new Wards() { DistrictId = 3, Name = "Phường 4"},
                    new Wards() { DistrictId = 3, Name = "Phường 5"},
                    new Wards() { DistrictId = 3, Name = "Phường 6"},
                    new Wards() { DistrictId = 3, Name = "Phường 7"},
                    new Wards() { DistrictId = 3, Name = "Phường 8"},
                    new Wards() { DistrictId = 3, Name = "Phường 9"},
                    new Wards() { DistrictId = 3, Name = "Phường 10"},
                    new Wards() { DistrictId = 3, Name = "Phường 11"},
                    new Wards() { DistrictId = 3, Name = "Phường 12"},
                    new Wards() { DistrictId = 3, Name = "Phường 13"},
                    new Wards() { DistrictId = 3, Name = "Phường 14"},

                    new Wards() { DistrictId = 4, Name = "Phường 1"},
                    new Wards() { DistrictId = 4, Name = "Phường 2"},
                    new Wards() { DistrictId = 4, Name = "Phường 3"},
                    new Wards() { DistrictId = 4, Name = "Phường 4"},
                    new Wards() { DistrictId = 4, Name = "Phường 5"},
                    new Wards() { DistrictId = 4, Name = "Phường 6"},
                    new Wards() { DistrictId = 4, Name = "Phường 8"},
                    new Wards() { DistrictId = 4, Name = "Phường 9"},
                    new Wards() { DistrictId = 4, Name = "Phường 10"},
                    new Wards() { DistrictId = 4, Name = "Phường 12"},
                    new Wards() { DistrictId = 4, Name = "Phường 13"},
                    new Wards() { DistrictId = 4, Name = "Phường 14"},
                    new Wards() { DistrictId = 4, Name = "Phường 15"},
                    new Wards() { DistrictId = 4, Name = "Phường 16"},
                    new Wards() { DistrictId = 4, Name = "Phường 18"},

                    new Wards() { DistrictId = 5, Name = "Phường 1"},
                    new Wards() { DistrictId = 5, Name = "Phường 2"},
                    new Wards() { DistrictId = 5, Name = "Phường 3"},
                    new Wards() { DistrictId = 5, Name = "Phường 4"},
                    new Wards() { DistrictId = 5, Name = "Phường 5"},
                    new Wards() { DistrictId = 5, Name = "Phường 6"},
                    new Wards() { DistrictId = 5, Name = "Phường 7"},
                    new Wards() { DistrictId = 5, Name = "Phường 8"},
                    new Wards() { DistrictId = 5, Name = "Phường 9"},
                    new Wards() { DistrictId = 5, Name = "Phường 10"},
                    new Wards() { DistrictId = 5, Name = "Phường 11"},
                    new Wards() { DistrictId = 5, Name = "Phường 12"},
                    new Wards() { DistrictId = 5, Name = "Phường 13"},
                    new Wards() { DistrictId = 5, Name = "Phường 14"},
                    new Wards() { DistrictId = 5, Name = "Phường 15"},

                    new Wards() { DistrictId = 6, Name = "Phường 1"},
                    new Wards() { DistrictId = 6, Name = "Phường 2"},
                    new Wards() { DistrictId = 6, Name = "Phường 3"},
                    new Wards() { DistrictId = 6, Name = "Phường 4"},
                    new Wards() { DistrictId = 6, Name = "Phường 5"},
                    new Wards() { DistrictId = 6, Name = "Phường 6"},
                    new Wards() { DistrictId = 6, Name = "Phường 7"},
                    new Wards() { DistrictId = 6, Name = "Phường 8"},
                    new Wards() { DistrictId = 6, Name = "Phường 9"},
                    new Wards() { DistrictId = 6, Name = "Phường 10"},
                    new Wards() { DistrictId = 6, Name = "Phường 11"},
                    new Wards() { DistrictId = 6, Name = "Phường 12"},
                    new Wards() { DistrictId = 6, Name = "Phường 13"},
                    new Wards() { DistrictId = 6, Name = "Phường 14"},

                    new Wards() { DistrictId = 7, Name = "Phường Phú Mỹ"},
                    new Wards() { DistrictId = 7, Name = "Phường Phú Thuận"},
                    new Wards() { DistrictId = 7, Name = "Phường Tân Phú"},
                    new Wards() { DistrictId = 7, Name = "Phường Tân Thuận Đông"},
                    new Wards() { DistrictId = 7, Name = "Phường Bình Thuận"},
                    new Wards() { DistrictId = 7, Name = "Phường Tân Thuận Tây"},
                    new Wards() { DistrictId = 7, Name = "Phường Tân Kiểng"},
                    new Wards() { DistrictId = 7, Name = "Phường Tân Quy"},
                    new Wards() { DistrictId = 7, Name = "Phường Tân Phong"},
                    new Wards() { DistrictId = 7, Name = "Phường Tân Hưng"},

                    new Wards() { DistrictId = 8, Name = "Phường 1"},
                    new Wards() { DistrictId = 8, Name = "Phường 2"},
                    new Wards() { DistrictId = 8, Name = "Phường 3"},
                    new Wards() { DistrictId = 8, Name = "Phường 4"},
                    new Wards() { DistrictId = 8, Name = "Phường 5"},
                    new Wards() { DistrictId = 8, Name = "Phường 6"},
                    new Wards() { DistrictId = 8, Name = "Phường 7"},
                    new Wards() { DistrictId = 8, Name = "Phường 8"},
                    new Wards() { DistrictId = 8, Name = "Phường 9"},
                    new Wards() { DistrictId = 8, Name = "Phường 10"},
                    new Wards() { DistrictId = 8, Name = "Phường 11"},
                    new Wards() { DistrictId = 8, Name = "Phường 12"},
                    new Wards() { DistrictId = 8, Name = "Phường 13"},
                    new Wards() { DistrictId = 8, Name = "Phường 14"},
                    new Wards() { DistrictId = 8, Name = "Phường 15"},
                    new Wards() { DistrictId = 8, Name = "Phường 16"},

                    new Wards() { DistrictId = 9, Name = "Phường Hiệp Phú"},
                    new Wards() { DistrictId = 9, Name = "Phường Long Bình"},
                    new Wards() { DistrictId = 9, Name = "Phường Long Phước"},
                    new Wards() { DistrictId = 9, Name = "Phường Long Thạnh Mỹ"},
                    new Wards() { DistrictId = 9, Name = "Phường Long Trường"},
                    new Wards() { DistrictId = 9, Name = "Phường Phú Hữu"},
                    new Wards() { DistrictId = 9, Name = "Phường Phước Bình"},
                    new Wards() { DistrictId = 9, Name = "Phường Phước Long A"},
                    new Wards() { DistrictId = 9, Name = "Phường Phước Long B"},
                    new Wards() { DistrictId = 9, Name = "Phường Tân Phú"},
                    new Wards() { DistrictId = 9, Name = "Phường Tăng Nhơn Phú A"},
                    new Wards() { DistrictId = 9, Name = "Phường Tăng Nhơn Phú B"},
                    new Wards() { DistrictId = 9, Name = "Phường Trường Thạnh"},

                    new Wards() { DistrictId = 10, Name = "Phường 1"},
                    new Wards() { DistrictId = 10, Name = "Phường 2"},
                    new Wards() { DistrictId = 10, Name = "Phường 3"},
                    new Wards() { DistrictId = 10, Name = "Phường 4"},
                    new Wards() { DistrictId = 10, Name = "Phường 5"},
                    new Wards() { DistrictId = 10, Name = "Phường 6"},
                    new Wards() { DistrictId = 10, Name = "Phường 7"},
                    new Wards() { DistrictId = 10, Name = "Phường 8"},
                    new Wards() { DistrictId = 10, Name = "Phường 9"},
                    new Wards() { DistrictId = 10, Name = "Phường 10"},
                    new Wards() { DistrictId = 10, Name = "Phường 11"},
                    new Wards() { DistrictId = 10, Name = "Phường 12"},
                    new Wards() { DistrictId = 10, Name = "Phường 13"},
                    new Wards() { DistrictId = 10, Name = "Phường 14"},
                    new Wards() { DistrictId = 10, Name = "Phường 15"},

                    new Wards() { DistrictId = 11, Name = "Phường 1"},
                    new Wards() { DistrictId = 11, Name = "Phường 2"},
                    new Wards() { DistrictId = 11, Name = "Phường 3"},
                    new Wards() { DistrictId = 11, Name = "Phường 4"},
                    new Wards() { DistrictId = 11, Name = "Phường 5"},
                    new Wards() { DistrictId = 11, Name = "Phường 6"},
                    new Wards() { DistrictId = 11, Name = "Phường 7"},
                    new Wards() { DistrictId = 11, Name = "Phường 8"},
                    new Wards() { DistrictId = 11, Name = "Phường 9"},
                    new Wards() { DistrictId = 11, Name = "Phường 10"},
                    new Wards() { DistrictId = 11, Name = "Phường 11"},
                    new Wards() { DistrictId = 11, Name = "Phường 12"},
                    new Wards() { DistrictId = 11, Name = "Phường 13"},
                    new Wards() { DistrictId = 11, Name = "Phường 14"},
                    new Wards() { DistrictId = 11, Name = "Phường 15"},
                    new Wards() { DistrictId = 11, Name = "Phường 16"},

                    new Wards() { DistrictId = 12, Name = "Phường An Phú Đông"},
                    new Wards() { DistrictId = 12, Name = "Phường Đông Hưng Thuận"},
                    new Wards() { DistrictId = 12, Name = "Phường Hiệp Thành"},
                    new Wards() { DistrictId = 12, Name = "Phường Tân Chánh Hiệp"},
                    new Wards() { DistrictId = 12, Name = "Phường Tân Hưng Thuận"},
                    new Wards() { DistrictId = 12, Name = "Phường Tân Thới Hiệp"},
                    new Wards() { DistrictId = 12, Name = "Phường Tân Thới Nhất"},
                    new Wards() { DistrictId = 12, Name = "Phường Thạnh Lộc"},
                    new Wards() { DistrictId = 12, Name = "Phường Thạnh Xuân"},
                    new Wards() { DistrictId = 12, Name = "Phường Thới An"},
                    new Wards() { DistrictId = 12, Name = "Phường Trung Mỹ Tây"},

                    new Wards() { DistrictId = 13, Name = "Phường 1"},
                    new Wards() { DistrictId = 13, Name = "Phường 2"},
                    new Wards() { DistrictId = 13, Name = "Phường 3"},
                    new Wards() { DistrictId = 13, Name = "Phường 5"},
                    new Wards() { DistrictId = 13, Name = "Phường 6"},
                    new Wards() { DistrictId = 13, Name = "Phường 7"},
                    new Wards() { DistrictId = 13, Name = "Phường 11"},
                    new Wards() { DistrictId = 13, Name = "Phường 12"},
                    new Wards() { DistrictId = 13, Name = "Phường 13"},
                    new Wards() { DistrictId = 13, Name = "Phường 14"},
                    new Wards() { DistrictId = 13, Name = "Phường 15"},
                    new Wards() { DistrictId = 13, Name = "Phường 17"},
                    new Wards() { DistrictId = 13, Name = "Phường 19"},
                    new Wards() { DistrictId = 13, Name = "Phường 21"},
                    new Wards() { DistrictId = 13, Name = "Phường 22"},
                    new Wards() { DistrictId = 13, Name = "Phường 24"},
                    new Wards() { DistrictId = 13, Name = "Phường 25"},
                    new Wards() { DistrictId = 13, Name = "Phường 26"},
                    new Wards() { DistrictId = 13, Name = "Phường 27"},

                    new Wards() { DistrictId = 14, Name = "Phường Linh Xuân"},
                    new Wards() { DistrictId = 14, Name = "Phường Bình Chiểu"},
                    new Wards() { DistrictId = 14, Name = "Phường Tam Bình"},
                    new Wards() { DistrictId = 14, Name = "Phường Tam Phú"},
                    new Wards() { DistrictId = 14, Name = "Phường Hiệp Bình Phước"},
                    new Wards() { DistrictId = 14, Name = "Phường Hiệp Bình Chánh"},
                    new Wards() { DistrictId = 14, Name = "Phường Linh Chiểu"},
                    new Wards() { DistrictId = 14, Name = "Phường Linh Tây"},
                    new Wards() { DistrictId = 14, Name = "Phường Linh Đông"},
                    new Wards() { DistrictId = 14, Name = "Phường Trường Thọ"},
                });
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if (_context.Properties.Count() == 0)
            {
                var adminId = _userManager.FindByNameAsync("admin").Result.Id;
                _context.Properties.AddRange(new List<Property>()
                    {
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                    new Property() { PropertyCategoryId = CommonConstantsProperty.Rooms, RentalTypeId = CommonConstantsProperty.ForRent, WardsId = 8, Title = "Modern and quirky flat", Desc = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium magnam veniam sit reprehenderit deserunt ad voluptates id aperiam veritatis! Nobis saepe quos eveniet numquam vitae quis, tenetur consectetur impedit dolore.", Price=2500000, Acreage=25, Address="01 Võ Văn Ngân", Status = Status.Active, UserId = adminId, Lat=  40.849150, Lng = -73.935100, Featured = false, Bedrooms = 1, Bathrooms = 1, Garages = 0},
                });
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if (_context.Features.Count() == 0)
            {
                _context.Features.AddRange(new List<Feature>() {
                    new Feature() {Name = "Air Conditioning"},
                    new Feature() {Name = "Barbeque"},
                    new Feature() {Name = "Dryer"},
                    new Feature() {Name = "Microwave"},
                    new Feature() {Name = "Fireplace"},
                    new Feature() {Name = "Sauna"},
                    new Feature() {Name = "TV Cable"},
                    new Feature() {Name = "WiFi"},
                });
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if (_context.PropertyImages.Count() == 0)
            {
                if (_context.Properties.Count() != 0)
                {
                    var properties = _context.Properties.ToList();
                    if (_context.PropertyImages.Count() == 0)
                    {
                        foreach (Property p in properties)
                        {
                            _context.PropertyImages.AddRange(new List<PropertyImage>()
                            {
                                new PropertyImage() { PropertyId = p.Id, PublicId = "e9780d84d0072401c6e8e5e01005c1a6", Url = "https://res.cloudinary.com/namqd98/image/upload/v1594612036/Default/1-big_ajhpdn.jpg" },
                                new PropertyImage() { PropertyId = p.Id, PublicId = "e9780d84d0072401c6e8e5e01005c1a6", Url = "https://res.cloudinary.com/namqd98/image/upload/v1594612036/Default/1-big_ajhpdn.jpg" },
                                new PropertyImage() { PropertyId = p.Id, PublicId = "e9780d84d0072401c6e8e5e01005c1a6", Url = "https://res.cloudinary.com/namqd98/image/upload/v1594612036/Default/1-big_ajhpdn.jpg" },
                                new PropertyImage() { PropertyId = p.Id, PublicId = "e9780d84d0072401c6e8e5e01005c1a6", Url = "https://res.cloudinary.com/namqd98/image/upload/v1594612036/Default/1-big_ajhpdn.jpg" },
                            });
                        }
                    }
                    if (_context.PropertyFeatures.Count() == 0)
                    {
                        foreach (Property p in properties)
                        {
                            foreach (Feature f in _context.Features.ToList())
                            {
                                _context.PropertyFeatures.Add(new PropertyFeature() { PropertyId = p.Id, FeatureId = f.Id });
                            }
                        }
                    }
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if (_context.Ratings.Count() == 0)
            {
                var properties = _context.Properties.ToList();
                var users = _context.Users.ToList();
                foreach (Property propery in properties)
                {
                    foreach (User user in users)
                    {
                        _context.Ratings.Add(new Rating() { PropertyId = propery.Id, UserId = user.Id, Value = 100 });
                    }
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if (_context.Comments.Count() == 0)
            {
                if (_context.Properties.Count() != 0)
                {
                    var user = _context.Users.ToList().FirstOrDefault();
                    var properties = _context.Properties.ToList();

                    foreach (Property p in properties)
                    {
                        _context.Comments.AddRange(new List<Comment>()
                            {
                                new Comment() { PropertyId = p.Id, UserId = user.Id, Content = "Tot day", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                                new Comment() { PropertyId = p.Id, UserId = user.Id, Content = "Tot day", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                                new Comment() { PropertyId = p.Id, UserId = user.Id, Content = "Tot day", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                                new Comment() { PropertyId = p.Id, UserId = user.Id, Content = "Tot day", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                            });
                    }
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

            if(_context.LogTypes.Count() == 0)
            {
                _context.LogTypes.AddRange(new List<LogType>() {
                    new LogType() {Name = "Property Deleted", Icon = "delete_forever"},
                    new LogType() {Name = "Property Edited", Icon = "create"},
                    new LogType() {Name = "Property Hided", Icon = "visibility_off"},
                    new LogType() {Name = "Property Unhided", Icon = "visibility"},
                    new LogType() {Name = "Property Approved", Icon = "check_circle"},
                    new LogType() {Name = "New Property Submitted", Icon = "publish"},
                    new LogType() {Name = "User Deleted", Icon = "person_remove"},
                    new LogType() {Name = "User Registed", Icon = "person_add"},  
                    new LogType() {Name = "Assigned Admin Role", Icon = "admin_panel_settings"},  
                    new LogType() {Name = "Assigned Client Role", Icon = "admin_panel_settings"},  
                });
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }
        }
    }
}