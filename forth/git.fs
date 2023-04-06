\ --------------------------------------------------------------------------------------------------------------------------------------------
\ Git-related words
\ --------------------------------------------------------------------------------------------------------------------------------------------

: git-init s" git init" system ;
: git-add-all s" git add --all" system ;
: git-commit s" git commit" system ;
: git-status s" clear && git status" system ;
: git-branch s" git branch -a" system ;
: git-log s" clear && git log --oneline --all --graph --decorate" system ;

