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

using DiscordServerListLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordServerListLib.Data
{
    public class DiscordServerRepository : IDiscordServerRepository
    {
        private readonly DiscordListDbContext _context;

        public DiscordServerRepository(DiscordListDbContext context)
        {
            _context = context;
        }

        public Task<List<DiscordServer>> GetDiscordServers()
        {
            return _context.DiscordServers.ToListAsync();
        }

        public async Task<DiscordServer> GetDiscordServerById(int id)
        {
            return await _context.DiscordServers.FindAsync(id);
        }

        public async Task InsertDiscordServer(DiscordServer server)
        {
            await _context.DiscordServers.AddAsync(server);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDiscordServer(int id)
        {
            DiscordServer server = await _context.DiscordServers.FindAsync(id);
            _context.DiscordServers.Remove(server);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDiscordServer(DiscordServer server)
        {
            _context.Entry(server).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
