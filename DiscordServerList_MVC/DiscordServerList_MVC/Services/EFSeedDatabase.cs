/*
MIT License
Copyright(c) 2021 Kyle Givler
https://github.com/JoyfulReaper
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using DiscordServerList_MVC.Enums;
using DiscordServerListLib.Data;
using DiscordServerListLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordServerList_MVC.Services
{
    public class EFSeedDatabase : ISeedDatabase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<DiscordUser> _userManager;
        private readonly IConfiguration _configuration;

        public EFSeedDatabase(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<DiscordUser> userManager,
            IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task SeedDatabase()
        {
            await _context.Database.MigrateAsync();
            await SeedRoles();
            await SeedUsers();
        }

        private async Task SeedRoles()
        {
            if (_context.Roles.Any())
            {
                return;
            }

            foreach (var role in Enum.GetNames(typeof(DiscordUserRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsers()
        {
            if (_context.Users.Any())
            {
                return;
            }

            var adminUser = new DiscordUser()
            {
                DisplayName = "Admin",
                FirstName = _configuration["AdminFirstName"],
                LastName = _configuration["AdminLastName"],
                Email = _configuration["AdminEmail"],
                UserName = _configuration["AdminEmail"],
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(adminUser, _configuration["InitialAdminPassword"]);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to seed Admin user! {result.Errors.FirstOrDefault().Description}");
            }

            await _userManager.AddToRoleAsync(adminUser, DiscordUserRoles.Administrator.ToString());
        }
    }
}
