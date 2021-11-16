## Workflow
How the miro board should not be a place where we dump everything, and what and what not to put there

The main takeaway is we will try a new approach :
- Every bug should be filed in a **Bitbucket Issue**, within its *own branch*
- Following that logic, every feature implementation should has its *own branch*, hopefully solving my (seb) problem of jumping from one task to another before finishing it
- Marie can then look at the branches and deduce which features has been worked on and has a better understanding of the project structure

* Related to that, Marie should be more comfortable with a git client of her choice in order to :
1. Facilitate testing when feature branches are in an experimental state (ie. before merging to the main branch)
2. Be more autonomous when it comes to resolve common git problems

___
## Tasks backlog
- Wiki (establish the current state of the game and put the features that are *in there*, **not the ones that should be there**
- First debugging pass where hacked solutions are implemented properly and most visible bugs get solved
- Remote controlled **game design**, writing down which features will benefit from remote control and how they are relevant to our game
  - High level concept of remote controlled ideas (without a necessary plan on how to do it)
  - At the end we want a multiple page online spreadsheet that is organized in a clear way (incl. visually)
    - think about how that will work, maybe even make test sheets (this would be super helpful)
    -  do a small one for next week with at least one feature
- Remote controlled **tech**, finish the script that fetches data from a spreadsheet and applies it to the game running (end goal : multiple game clients are updated at the same time when a value is adjusted )
- Für Marie : possibly look at different git clients and pick one after deliberation