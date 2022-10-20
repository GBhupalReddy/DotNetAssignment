var date = new Date();

var day = date.getDate();
var month = date.getMonth() + 1;
var year = date.getFullYear();
var todaydate = day;
if (month < 10) month = "0" + month;
if (day < 10) day = "0" + day;

var today = year + "-" + month + "-" + day;

if(todaydate < 3) todaydate = "0" + todaydate+6;
if(day < 32 && day > 2) todaydate = todaydate+6;

var maxdate = year + "-" + month + "-" + todaydate;

document.getElementById('selectedDate').value = today;

document.getElementById('selectedDate').setAttribute("min", today)
document.getElementById('selectedDate').setAttribute("max", maxdate)

var dataselected='';
function dateseletedr()
{
   

    dataselected = document.getElementById("selectedDate").value


}



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

     dataselected = document.getElementById("selectedDate").value
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
                        <button class="Book" onclick="showtimes('${moviename}','${dataselected}')">Booking</button>
                    </div>
                </div>
                <div class="aboutmovie" >
                    <h2>About the movie</h2>
                   <p>${description}</p>
                </div>
             </div>
            `;
            $('.Home').append(div);
        
    
}

function showtimes(movieName,date)
{
    row=' '
    // table1 = 
    table =   document.querySelector('#showtimeings')
    document.querySelector(".Home").innerHTML = '';

        var city = $('#citySelect :selected').val();
    
        var url = 'https://localhost:7119/movie/movies-'+ city;
        url += '?movieName='+ movieName;
           
        if(date)
          url +='&date='+date;

        console.log(url);

         $.get(url , function(data, status){
            table.innerHTML=' '
            row =`<tr class = "Headings">
            <td><h3>cinemaName</h3></td>
            <td><h3>cinemaHallName</h3></td>
            <td><h3>showTiming</h3></td>
            <td><h3>Date</h3></td>
            </tr>`
    table.innerHTML +=row;
            for(let i=0;i<data.length;i++)
            {
            row =`<tr>
                <td><h3>${data[i].cinemaName}</h3></td>
                <td><h3>${data[i].cinemaHallName}</h3></td>
                <td><h3>${data[i].showTiming}</h3></td>
                <td><h3>${date}</h3></td>
                <td><button onclick="selectseat('${data[i].showId}')">Book</button></td>
                </tr>`
        table.innerHTML +=row;

     }

    });
        
}
function selectseat(showid)
{
    document.querySelector(".table").innerHTML = '';

     
    console.log(showid);
    document.querySelector(".selectseat").innerHTML = '';
    let div = document.createElement('div');
        div.className = "movedetails";

        div.innerHTML = `
        <ul class="showcase">
    <li>
      <div class="seat"></div>
      <small>N/A</small>
    </li>

    <li>
      <div class="seat selected"></div>
      <small>Selected</small>
    </li>

    <li>
      <div class="seat occupied"></div>
      <small>Occupied</small>
    </li>
  </ul>
  <div class="container">
    <div class="row">
      <div class="seat" id="1">1</div>
      <div class="seat" id="2">2</div>
    </div>
    <div class="row">
      <div class="seat" id="4">3</div>
      <div class="seat" id="4">4</div>
    </div>
    <div class="row">
      <div class="seat" id="5">5</div>
      <div class="seat" id="6">6</div>
    </div>
    </div>
    <p class="text">
      You have selected <span id="count">0</span> seats for a price of $<span id="total">0</span>
    </p>
    `;
      
        $('.selectseat').append(div);
   // window.location.assign("selectseat.html")
    console.log(showid);
    
}









