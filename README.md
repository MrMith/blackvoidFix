# blackvoidFix
Fixes glitch for SCP:SL start of round glitch for players.
This plugin looks if someone died at the start of the round then kicks them telling them to please reconnect.
When they rejoin they get the class they had.

## Install Instructions.
Put blackvoidFix.dll under the release tab into sm_plugins folder.


## Config Options.
| Config Option              | Value Type      | Default Value | Description |
|   :---:                    |     :---:       |    :---:      |    :---:    |
| void_secondstorespawn      | Int             | 5             | How many seconds till this plugin stops respawning people. |
| void_disable               | Boolean         | False         | Disables the entire blackvoidFix plugin.    |

## Commands

| Command(s)                 | Value Type      | Description                              |
|   :---:                    |     :---:       |    :---:                                 |
| void_version               | N/A             | Get the version of this plugin           |
| void_disable               | N/A             | Disables the entire blackvoidFix plugin.    |