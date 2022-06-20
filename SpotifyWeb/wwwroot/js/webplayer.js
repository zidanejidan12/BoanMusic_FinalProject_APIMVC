const musicContainer = document.querySelector('.music-container')
const playBtn = document.querySelector('#play')
const prevBtn = document.querySelector('#prev')
const nextBtn = document.querySelector('#next')
const audio = document.querySelector('#audio')
const progress = document.querySelector('.progress')
const progressContainer = document.querySelector('.progress-container')
const title = document.querySelector('#title')
const cover = document.querySelector('#cover')
const slider = document.querySelector('input');

//Song Titles
//const songs = getItems();
//var path = window.location.pathname;
//var page = path.split("/").pop();

//Keep track of songs
let songIndex = 0

//Initially load song info DOM
let songs;
loadSong();


//Update song details
async function loadSong() {
    songs = await getSongs();
    song = songs[songIndex];
    title.innerText = song['title']
    audio.src = `../music/${song['songMP3']}`
    cover.src = song['imageSongURL']
}

function playSong() {
    musicContainer.classList.add('play')
    playBtn.querySelector('i.fas').classList.remove('fa-play')
    playBtn.querySelector('i.fas').classList.add('fa-pause')
    audio.play()
}

function pauseSong() {
    musicContainer.classList.remove('play')
    playBtn.querySelector('i.fas').classList.add('fa-play')
    playBtn.querySelector('i.fas').classList.remove('fa-pause')

    audio.pause()
}

async function prevSong() {
    songIndex--
    songs = await getSongs();
    if (songIndex < 0) {
        songIndex = songs.length - 1;
    }
    await loadSong();    
    await playSong();

    
}

async function nextSong() {
    songIndex++
    songs = await getSongs();
    if (songIndex > songs.length - 1) {
        songIndex = 0;
    }
    await loadSong();
    await playSong();
}

function updateProgress(e) {
    const { duration, currentTime } = e.srcElement
    const progressPercent = (currentTime / duration) * 100
    progress.style.width = `${progressPercent}%`
}

function setProgress(e) {
    const width = this.clientWidth
    const clickX = e.offsetX
    const duration = audio.duration

    audio.currentTime = (clickX/width) * duration
}

slider.oninput = function(){
    progressBar = document.querySelector('progress');
    progressBar.value = slider.value;
    sliderValue = document.querySelector('.sliderValue');
    sliderValue.innerHTML = slider.value;
    audio.volume = slider.value / 100;

}

async function getSongs() {
    try {
        let url = 'https://localhost:44300/api/v1/songs/';
        let res = await fetch(url);
        return await res.json();
        //loadSong(songs[songIndex]);
        
    } catch (error) {
        console.log(error);
    }
}

//async function getSong(song) {
//    try {
//        let baseIndex = songIndex;
//        let counter = 0;
//        let url = 'https://localhost:44300/api/v1/songs/' + song;
//        let res = await fetch(url);
//        if (!res.ok) {
//            if (counter == 10) {
//                songIndex = baseIndex;
//                return;
//            }
//            songIndex++;
//            counter++;
//            getSong();
//        }
//        return await res.json();
//        //loadSong(songs[songIndex]);

//    } catch (error) {
//        console.log(error);
//    }
//}

//Event listeners
playBtn.addEventListener('click', () => {
    const isPlaying = musicContainer.classList.contains('play')

    if (isPlaying) {
        pauseSong()
    } else {
        playSong()
    }
})

//Change song events
prevBtn.addEventListener('click', prevSong);
nextBtn.addEventListener('click', nextSong);

audio.addEventListener('timeupdate', updateProgress);

progressContainer.addEventListener('click', setProgress)
audio.addEventListener('ended', nextSong)


