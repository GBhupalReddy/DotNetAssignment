
function getmovies(){
    
    document.querySelector(".HomeMovie").innerHTML = '';
    document.querySelector(".movies").innerHTML = '';
    var language =$('#LanguagesSelect :selected').val();
    var city = $('#citySelect :selected').val();
    var genre = $('#GenresSelect :selected').val();
    let heading = document.createElement('div');
        heading.className = "headingss";
        heading.innerHTML = `<h1 class="cityheading">Movies In ${city}</h1>`;
        $('.movies').append(heading);
        
    var url = 'https://localhost:7119/city/movies/' + city;
    if(language && genre)
    {
        url += '?language='+ language+'&genre=' + genre;
    
    }
    else if(language)
    {
        url += '?language='  + language;
    }
    else if(genre)
    {
        url += '?genre='+genre
    }

   
    $.get(url , function(data, status){
        for (let item of data) {
             let div = document.createElement('div');
            div.className = "movie";
            div.innerHTML = `
            <div class=${item.tittle}>
            <img src="${item.imgPath}" alt=${item.tittle} onclick="onimageclick('${item.tittle}','${item.imgPath}','${item.language}','${item.description}')" >
            <h1 class="tittle">${item.tittle}</h1>
            <h4 class="duraction">${item.duration}</h4>
            <h4 class="Laguage">${item.language}</h4>
            <h4 class="RealseDate">${item.releaseDate}</h4>
            <h4 class="Country">${item.country}</h4>
            <h4 class="genre">${item.genre}</h4>
            </div>
            `;
            $('.HomeMovie').append(div);
        }
        // console.log("Data: " + JSON.stringify(data) + "\nStatus: " + status);
      });
      
}

function onimageclick(moviename,imagepath,language,description)
{
    
        document.querySelector(".Home").innerHTML = '';
        let div = document.createElement('div');
            div.className = "movedetails";

            div.innerHTML = `
            <div id="${moviename}">
                <div class="moviehead"></div>
                   <div class="movielogo">
                      <img src="${imagepath}" alt="">
                   </div>
                    <div class="booking">
                        <h1>${moviename}</h1>
                        <h3>${language}</h3>
                        <button class="Book" onclick="showtimes('${moviename}')">Booking</button>
                    </div>
                </div>
                <div class="aboutnovie" >
                    <h2>About the movie</h2>
                   <p>${description}</p>
                </div>
             </div>
            `;
            $('.Home').append(div);
        
    
}

function showtimes(movieName)
{
    row=' '
    // table1 = 
    table =   document.querySelector('#showtimeings')
    document.querySelector(".Home").innerHTML = '';

        var city = $('#citySelect :selected').val();
    
        var url = 'https://localhost:7119/movie/movies-'+ city;
        url += '?movieName='+ movieName;

        console.log(url);

         $.get(url , function(data, status){
            table.innerHTML=' '
            row =`<tr class = "Headings">
            <td><h3>cinemaName</h3></td>
            <td><h3>cinemaHallName</h3></td>
            <td><h3>showTiming</h3></td>
            </tr>`
    table.innerHTML +=row;
            for(let i=0;i<data.length;i++)
            {
            row =`<tr>
                <td><h3>${data[i].cinemaName}</h3></td>
                <td><h3>${data[i].cinemaHallName}</h3></td>
                <td><h3>${data[i].showTiming}</h3></td>
                <td><button onclick="selectseat('${data[i].showId}')">Book</button></td>
                </tr>`
        table.innerHTML +=row;

     }

    });
        
}
function selectseat(showid)
{
    console.log(showid);
    window.location.assign("selectseat.html")
}









