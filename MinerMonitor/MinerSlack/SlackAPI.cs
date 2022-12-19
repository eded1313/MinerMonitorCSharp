using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MinerSlack
{
    public class SlackAPI : SlackClient
    {
        public SlackAPI(string channel) : base(channel)
        {
        }

        public override async Task<bool> ExecuteAsync(SlackMessageOptionModel message)
        {
            try
            {
                if (!await this.SendMessageAsync(message))
                {
                    Console.WriteLine("Fail Send Message");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
