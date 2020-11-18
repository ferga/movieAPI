Running this program requires Visul Studio 2019 or similar environment supporting .NET Core 2.2 and above.
However, it has been only tested on Visual Studio 2019...

- Opening this repository , it should be sufficient to hit F5 and the environment will open a new window of your default browser, showing just 
a blank page.
- Attention, some times, it is possible to get the expired credentials message or a prompt to confirm whether we trust this address or not.
The address it tries to access is on localhost, thus, it should be completely safe to click on trust and proceed.
- From then, you can just add at the end of the page url "/Movies/stats" and hit enter, to see the statistics from the Stats csv file.
--
-Similarly you can visit https://localhost:44353/Metadata/3 to see all movies with movieID=3
--
--
--
- In order to fulfil post requests though, things are a bit more complicated, as browsers do not support post requests. A common software that 
I used is Postman. You can use any other you like, but that is the one I used for my tests.
- Opening Postman, there is a big input box at the top of the program, with a button just in front of it, probably saying "Get"
Change it so it says Post.
Then next to it, past this URL "https://localhost:44353/metadata". Please notice, that the port of the localhost could be different,
on this case, copy the number of the opened browser window, when you clicked F5 in visual studio.
Next, from the line of options under the input box, click to the one named "Body" and then below that, select the "raw" option.
On the space below add a JSon object like this...
{
"movieId": 3,
"title": "Elysium",
"language": "EN",
"duration": "1:49:00",
"releaseYear": 2013
}
and click send

That should return something similar to the below, along with the status "200 Ok"
{
    "token": 27
}

--- Note, Posting will not have an affect to the amount of entries in the list, as the repositories are not static. Making them
static would have them keep the new values until the project have been restarted, but that was out of this project's scope