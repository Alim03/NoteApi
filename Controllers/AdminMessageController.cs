using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Challenge.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

namespace Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminMessageController : ControllerBase
    {
        private readonly static Queue<JObject> queue = new Queue<JObject>();
        private readonly IHubContext<AdminCallCenterHub, IAdminCallCenterHub> _hubContext;
        public AdminMessageController(IHubContext<AdminCallCenterHub, IAdminCallCenterHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JObject data)
        {
            if (data["message"] != null)
            {
                await _hubContext.Clients.All.MessageAdded(data["message"].ToString());
            }
            queue.Enqueue(data);
            if (queue.Count >= 10)
            {
                var list = queue.ToList();
                queue.Clear();
                await _hubContext.Clients.All.QueueClear();
                return Ok(list);
            }
            return NoContent();
        }
    }
}