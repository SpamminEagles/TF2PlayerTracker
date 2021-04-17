/**
 * ======================================
 * TF2 Player Tracker player info polling
 * ======================================
 *
 * Header declaring contents for the polling source.
 * 
 * Author(s):
 *      SpamminRagles
 * License(s): 
 *      MIT
 */
#if defined _playertracker_polling_
    #endinput
#endif
#define _playertracker_polling_

/**
 * Executes a poll for user data
 *
 * @param timer Handle for the timer
 * @return the action
 */
forward Action PollPlayerData(Handle timer);
