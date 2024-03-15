### Idea

If we want to trigger deployments to self-hosted servers using github actions, must keep our project repo private. As otherwise we would conflict with githubs security recommendations.

GitAndDeploy allows you to have a private repo containing the actions, but still only use one command to push to the (public) project repo AND at the same time trigger the (private) github action.


### Setup

Create a private repository that has set up a github action. Use On: push in order to have the workflow react to GitAndDeploy's push. In the actions we can trigger a self-hosted runner that is our server. We can run commands inside the runner to deploy the latest version of a public repository.

Clone that repository onto your machine into any directory. This directory needs to be set as the GitHookPath in the settings of GitAndDeploy.

You further need to set the Branch of the repo containing the action. The default is 'main'.

You can set a custom commit message. GitAndDeploy will use it to do its git commits.