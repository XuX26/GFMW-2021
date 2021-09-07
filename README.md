Please read this before working on the project !

> GIT <

-- Branch --
> Workflow
	- Master : the latest approved version, no bugs, only merge approved features
	- Dev : the main work branch, used for merge and test every changes
	- [name] (others) : create a new branch for each features
	- Merge : to merge commit from the other repo


> Commit convention
commit msg :		[keyword(s)] [TA]? : [title]
commit description :	short description (wich features/bug/ect..)   // Optionnal

keywords :
- feat	: new feature
- fix	: fix a bug
- LD 	: editing scene/prefabs and integration
- asset	: import new in game assets

- perf	: optimization (load/fps/importation/huge code improvment)
- doc	: add a new doc or comments in code
- refactor : editing code for make it cleaner
- debug : tools or display for debugging
- import : import other files (different that doc or assets) like code packages
- clean : organize files hierarchy

- TA	: tested and approved by the developper. Only for keywords : feat/LD

Exemple : [feat/fix] PlayerController + fix index error NbrPlayers
	  [LD/assets] Player Prefabs + Import player animations


Kiss by Lowkey <3