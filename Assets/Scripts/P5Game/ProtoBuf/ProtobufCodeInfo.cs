using System;
using System.Collections.Generic;
using Google.Protobuf;
using Game.Qe.Common.Msg.Proto;

public static class ProtobufCodeInfo
{
    public static Dictionary<short, Func<byte[], IMessage>> ProtoCodeToType = new Dictionary<short, Func<byte[], IMessage>>()
    {
        { 1001,(data)=>CS_login_msg.Parser.ParseFrom(data) },
        { 1002,(data)=>SC_login_msg.Parser.ParseFrom(data) },
        { 1005,(data)=>CS_phone_sms_msg.Parser.ParseFrom(data) },
        { 1006,(data)=>SC_phone_sms_msg.Parser.ParseFrom(data) },
        { 1007,(data)=>CS_phone_bind_msg.Parser.ParseFrom(data) },
        { 1008,(data)=>SC_phone_bind_msg.Parser.ParseFrom(data) },
        { 2001,(data)=>CS_role_list_msg.Parser.ParseFrom(data) },
        { 2002,(data)=>SC_role_list_msg.Parser.ParseFrom(data) },
        { 2003,(data)=>CS_role_create_msg.Parser.ParseFrom(data) },
        { 2004,(data)=>SC_role_create_msg.Parser.ParseFrom(data) },
        { 2005,(data)=>CS_role_enter_msg.Parser.ParseFrom(data) },
        { 2006,(data)=>SC_role_enter_msg.Parser.ParseFrom(data) },
        { 2007,(data)=>CS_role_face_msg.Parser.ParseFrom(data) },
        { 2008,(data)=>SC_role_face_msg.Parser.ParseFrom(data) },
        { 2009,(data)=>CS_role_transfer_msg.Parser.ParseFrom(data) },
        { 2010,(data)=>SC_role_transfer_msg.Parser.ParseFrom(data) },
        { 2011,(data)=>CS_role_rename_msg.Parser.ParseFrom(data) },
        { 2012,(data)=>SC_role_rename_msg.Parser.ParseFrom(data) },
        { 2013,(data)=>SC_role_complaint_msg.Parser.ParseFrom(data) },
        { 2014,(data)=>CS_role_complaint_msg.Parser.ParseFrom(data) },
        { 2015,(data)=>CS_role_afk_msg.Parser.ParseFrom(data) },
        { 2016,(data)=>SC_role_afk_msg.Parser.ParseFrom(data) },
        { 2017,(data)=>CS_role_delete_msg.Parser.ParseFrom(data) },
        { 2018,(data)=>SC_role_delete_msg.Parser.ParseFrom(data) },
        
    };

    public static Dictionary<string, short> ProtoTypeToCode = new Dictionary<string, short>()
    {
        {"CS_login_msg",1001},
        {"SC_login_msg",1002},
        {"CS_phone_sms_msg",1005},
        {"SC_phone_sms_msg",1006},
        {"CS_phone_bind_msg",1007},
        {"SC_phone_bind_msg",1008},
        {"CS_role_list_msg",2001},
        {"SC_role_list_msg",2002},
        {"CS_role_create_msg",2003},
        {"SC_role_create_msg",2004},
        {"CS_role_enter_msg",2005},
        {"SC_role_enter_msg",2006},
        {"CS_role_face_msg",2007},
        {"SC_role_face_msg",2008},
        {"CS_role_transfer_msg",2009},
        {"SC_role_transfer_msg",2010},
        {"CS_role_rename_msg",2011},
        {"SC_role_rename_msg",2012},
        {"SC_role_complaint_msg",2013},
        {"CS_role_complaint_msg",2014},
        {"CS_role_afk_msg",2015},
        {"SC_role_afk_msg",2016},
        {"CS_role_delete_msg",2017},
        {"SC_role_delete_msg",2018},
        
    };
}