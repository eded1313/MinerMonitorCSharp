using System;
using System.Collections.Generic;
using System.Text;

namespace MinerSlack
{
    public enum ResultType
    {
        PRIMARY,
        INFO,
        SUCCESS,
        WARNING,
        DANGER
    }

    static public class SlackChannel
    {
        public static string SLACK_WEBHOOK = "https://hooks.slack.com/services/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";
        public static string TEST_SLACK_WEBHOOK = "https://hooks.slack.com/services/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";
        public static string NASMG_SLACK_WEBHOOK = "https://hooks.slack.com/services/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";
    }

    static public class SlackOption
    {
        public static string GetSlackMsgColor(ResultType type)
        {
            string color = string.Empty;
            switch (type)
            {
                case ResultType.PRIMARY:
                    color = "#007bff";
                    break;
                case ResultType.INFO:
                    color = "#17a2b8";
                    break;
                case ResultType.SUCCESS:
                    color = "#28a745";
                    break;
                case ResultType.WARNING:
                    color = "#ffc107";
                    break;
                case ResultType.DANGER:
                    color = "#dc3545";
                    break;
            }
            return color;
        }

        public static SlackMessageOptionModel MakeSlackOption(string color, string title, string text)
        {
            return new SlackMessageOptionModel()
            {
                color = color,
                text = text,
                title = title
            };
        }
    }
}
