using MinerDaemon.Enum;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinerDaemon.Helper
{
    public static class CommandHelper
    {
        public static K FindKeyByValue<K, V>(this Dictionary<K, V> dict, V val)
        {
            return dict.FirstOrDefault(entry =>
                EqualityComparer<V>.Default.Equals(entry.Value, val)).Key;
        }

        public static Dictionary<CommandEnum, string> MonitoringCommand = new Dictionary<CommandEnum, string>()
        {
            {CommandEnum.WORKER_STATE_CMD, "lotus-miner sealing workers | grep dis"},
            {CommandEnum.GPU_STATE_CMD, "nvidia-smi | grep Version"},
            {CommandEnum.DISK_STATE_CMD, "df -h | grep lotus$"},
            {CommandEnum.SYNC_STATE_CMD, "lotus sync wait | grep Done"},
            // SOCAT_STATE_UNKNOWN_CMD 명령어 제외 [2022.12.18]
            //{CommandEnum.SOCAT_STATE_UNKNOWN_CMD, "lotus-miner net reachability | grep Unknown"},
            {CommandEnum.CHECK_EXPIRE, "lotus-miner sectors check-expire |  wc -l"},
            {CommandEnum.SECTOR_FAULT, "lotus-miner info | grep '32 GiB Faulty'" },
        };

        public static Dictionary<ExecuteType, List<CommandEnum>> CommandExeTime = new Dictionary<ExecuteType, List<CommandEnum>>()
        {
            { ExecuteType.EVERYTIME, new List<CommandEnum>(){ 
                CommandEnum.WORKER_STATE_CMD,
                CommandEnum.SYNC_STATE_CMD,
                CommandEnum.SECTOR_FAULT
            } },
            { ExecuteType.ONCE, new List<CommandEnum>(){ 
                CommandEnum.GPU_STATE_CMD,
                CommandEnum.DISK_STATE_CMD,
                CommandEnum.CHECK_EXPIRE
            } },
        };
    }
}
