/**
 * TF2 Player Tracker main program.
 * This is the main code file for the plugin.
 * 
 * Author(s):
 *      SpamminRagles
 */
#include <sourcemod>

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
    PrintToServer("TF2PlayerTracker loaded!");
}


