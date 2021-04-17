/**
 * ===============================
 * TF2 Player Tracker main program
 * ===============================
 *
 * This is the main code file for the plugin.
 * 
 * Author(s):
 *      SpamminRagles
 * License(s): 
 *      MIT
 */

#include <sourcemod>
#include <playertracker>
#include <playertracker_polling>

/**
 * Plugin public information.
 */
public Plugin myinfo =
{
	name = "TF2 Player Tracker",
	author = "SpaminEagles",
	description = "Player tracking for Team Fortress 2",
	version = "1.0",
	url = "https://github.com/SpamminEagles/TF2PlayerTracker"
};

/// Init
public void OnPluginStart()
{
    // Load start
    PrintToServer("Loading TF2PlayerTracker!");

    // Declaring cvars
    // endpoint
    g_cvarPlayerTrackerEndpoint = CreateConVar(
        CVAR_PLAYERTRACKER_APPSERVER_ENDPOINT,
        "file://localhost/dev/null",
        "Endpoint URL for Player Tracker to send the collected info to",
        FCVAR_PROTECTED
    )
    // authentication key
    g_PlayerTrackercvarAuth = CreateConVar(
        CVAR_PLAYERTRACKER_APPSERVER_AUTHKEY, 
        "", 
        "Key used for Basic HTTP authentication on the AppServer " ... 
            "by Player Tracker",
        FCVAR_PROTECTED
    )
    // polling interval
    g_PlayerTrackerPollInt = CreateConVar(
        CVAR_PLAYERTRACKER_POLLINT,
        "1.0",
        "Polling interval in seconds for Player Tracker",
        FCVAR_NOTIFY
    )
    // polling enabler flag
    g_PlayerTrackerPollEnabled = CreateConVar(
        CVAR_PLAYERTRACKER_POLLENABLED,
        "0",
        "Polling enabling flag for Player Tracker [bool]",
        FCVAR_NOTIFY
    )

    AutoExecConfig(true, "plugin_playertracker");

    // Create commands

    // get/set polling interval
    RegAdminCmd(
        CVAR_PLAYERTRACKER_POLLINT,
        Command_PollInt,
        ADMFLAG_GENERIC | ADMFLAG_CONVARS | ADMFLAG_CONFIG,
        "Admin command for querying and setting " ... 
            CVAR_PLAYERTRACKER_POLLINT ... " [float]"
    )
    // get/set polling enabled flag
    RegAdminCmd(
        CVAR_PLAYERTRACKER_POLLENABLED,
        Command_PollEnabled,
        ADMFLAG_GENERIC | ADMFLAG_CONVARS | ADMFLAG_CONFIG,
        "Admin command for querying and setting " ... 
            CVAR_PLAYERTRACKER_POLLENABLED ... " [boolean]"
    )

    // Creating timers
    CreateTimer(
        g_PlayerTrackerPollInt.FloatValue,
        PollPlayerData, 
        _, 
        TIMER_REPEAT
    );

    // Load start
    PrintToServer("TF2PlayerTracker loaded succesfully");

}

/**
 * *****************************************************************************
 * COMMANDS
 * *****************************************************************************
 */

public Action Command_PollInt(int client, int args){

    char valueStr[64];
    float newValue;

    // querying the value
    if (args == 0){
        ReplyToCommand(
            client, 
            "The polling interval is %f!", 
            g_PlayerTrackerPollInt.FloatValue
        );
    }
    // setting the value
    else if (args == 1) {
        GetCmdArg(1, valueStr, sizeof(valueStr));
        StringToFloatEx(valueStr, newValue);
        
        // validation
        if (newValue <= 0.0){
            ReplyToCommand(
                client, 
                "ERROR! supplied value '%s' is invalid or <0.0! " ...
                    "Please supply a valid floating point value, " ... 
                    "greater than 0.0!",
                valueStr
            );
        }
        // actual setting of the value
        else {
            g_PlayerTrackerPollInt.FloatValue = newValue;
            
            // validating update
            if (g_PlayerTrackerPollInt.FloatValue == newValue){
                ReplyToCommand(
                    client,
                    "Successfully changed the pollingrate to %f!",
                    newValue
                );
            }
            else {
                ReplyToCommand(
                    client,
                    "FAILED to change the polling rate for some reason! " ... 
                        "Current value is: %f",
                    g_PlayerTrackerPollInt.FloatValue
                );
            }
        }
    }
    else {
        ReplyToCommand(
            client,
            "Too many arguments passed!"
        );
    }

    return Plugin_Handled;
}

public Action Command_PollEnabled(int client, int args){
    char enabledStr[6];

    // querying value
    if (args == 0){
        ReplyToCommand(
            client,
            "Player Tracker polling is %s",
            g_PlayerTrackerPollEnabled.BoolValue ? "enabled" : "not enabled"
        )
    }
    // setting value
    else if (args == 1) {
        GetCmdArg(1, enabledStr, sizeof(enabledStr));
        bool enabled = StringToBool(enabledStr);
        g_PlayerTrackerPollEnabled.BoolValue = enabled;

        if (g_PlayerTrackerPollEnabled.BoolValue == enabled){
            ReplyToCommand(
                client,
                "Successfully changed %s to %s",
                CVAR_PLAYERTRACKER_POLLENABLED,
                g_PlayerTrackerPollEnabled.BoolValue ? "enabled" : "not enabled"
            )
        }
        else {
            ReplyToCommand(
                client,
                "FAILED to update " ... CVAR_PLAYERTRACKER_POLLENABLED ...
                    ", current value is %s",
                g_PlayerTrackerPollEnabled.BoolValue ? "enabled" : "not enabled"
            )
        }
    }
    else {
        ReplyToCommand(
            client,
            "Too many arguments passed!"
        );
    }

    return Plugin_Handled;
}

/**
 * *****************************************************************************
 * EVENTS
 * *****************************************************************************
 */

public Action PollPlayerData(Handle timer){
    if (g_PlayerTrackerPollEnabled.BoolValue){
        float originVec[3];
        float angleVec[3];

        for (int i = 1; i < MaxClients; i++){
            if (IsClientInGame(i)){
                GetClientAbsOrigin(i, originVec);
                GetClientAbsAngles(i, angleVec);

                PrintToChat(
                    i, 
                    "You are being polled! " ... 
                        "Your position is (%f, %f, %f), " ...
                        "Your angles are (%f, %f, %f).",
                        originVec[0],
                        originVec[1],
                        originVec[2],
                        angleVec[0],
                        angleVec[1],
                        angleVec[2]
                );
            }
        }

    }

    return Plugin_Continue;
}

/**
 * *****************************************************************************
 * HELPERS
 * *****************************************************************************
 */

bool StringToBool(char[] input){
    for (int i = 0; input[i] != 0; i++){
        input[i] = CharToLower(input[i]);
    }

    return !StrEqual(input, "0") && !StrEqual(input, "false");
}
