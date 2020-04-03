/********************************************************************
	Created:	2018-08-26
	Filename: 	EventMsgBody.cs
	Author:		Heq
	QQ:         372058864
*********************************************************************/

namespace HG
{
    public class EventMsgBody
    {
        public string EventId;
        public object[] Args;

        public EventMsgBody(string eventId, object[] para)
        {
            EventId = eventId;
            Args = para;
        }
    }
}