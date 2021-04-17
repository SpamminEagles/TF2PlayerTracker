/**
 * ===============================
 * PlayerTracker main include file
 * ===============================
 * 
 * Author(s):
 *      SpamminRagles
 * License(s): 
 *      MIT
 */

#if defined _playertracker_playertracker_
    #endinput
#endif
#define _playertracker_playertracker_

/**
 * Defines for convar names
 */

#define CVAR_PLAYERTRACKER_APPSERVER_ENDPOINT \
    "cv_playertracker_appserver_endpoint"

#define CVAR_PLAYERTRACKER_APPSERVER_AUTHKEY \
    "cv_playertracker_appserver_authkey"

#define CVAR_PLAYERTRACKER_POLLINT \
    "cv_playertracker_pollint"

#define CVAR_PLAYERTRACKER_POLLENABLED \
    "cv_playertracker_pollenabled"

/**
 * *****************************************************************************
 * *****************************************************************************
 * Global declarations
 * *****************************************************************************
 * *****************************************************************************
 */

/**
 * ConVars
 */

/**
 * g_cvarPlayerTrackerEndpoint - \ 
 *      Endpoint URL for Player Tracker to send the collected info to [string]
 */
ConVar g_cvarPlayerTrackerEndpoint;

/** 
 * g_PlayerTrackercvarAuth - \
 *      Key used for Basic HTTP authentication on the AppServer \
 *      by Player Tracker [string]
 */
ConVar g_PlayerTrackercvarAuth;

/**
 * g_PlayerTrackercvarPollInt - \
 *      Polling interval in seconds for Player Tracker [float]
 */
ConVar g_PlayerTrackerPollInt;

/** 
 * g_PlayerTrackercvarPollEnabled - \
 *      Polling enabling flag for Player Tracker [bool]
 */
ConVar g_PlayerTrackerPollEnabled;

/**
 * Gets or sets the value of @see g_PlayerTrackerPollInt
 * COMMAND syntax: `cv_playertracker_pollint [interval: float]`
 *
 * @param client the client id initiating the command
 * @args number of args
 * @returns action
 */
forward Action Command_PollInt(int client, int args);
/**
 * Gets or sets the value of @see g_PlayerTrackerPollEnabled
 * COMMAND syntax: `cv_playertracker_pollenabled [enabled: bool]`
 *
 * @param client the client id initiating the command
 * @args number of args
 * @returns action
 */
forward Action Command_PollEnabled(int client, int args);
