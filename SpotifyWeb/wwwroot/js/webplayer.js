const musicContainer = document.querySelector('.music-container')
const playBtn = document.querySelector('#play')
const prevBtn = document.querySelector('#prev')
const nextBtn = document.querySelector('#next')
const muteBtn = document.querySelector('#mute')
const audio = document.querySelector('#audio')
const pointer = document.querySelector('.pointer');
const progress = document.querySelector('.progress')
const progressContainer = document.querySelector('.progress-container')
const title = document.querySelector('#title')
const cover = document.querySelector('#cover')
const slider = document.querySelector('input');
const progressBar = document.querySelector('progress');
const imgCardInfo1 = document.querySelector('#imgCardInfo1');
const titleCardInfo1 = document.querySelector('#titleCardInfo1');
const artistCardInfo1 = document.querySelector('#artistCardInfo1');
const imgCardInfo2 = document.querySelector('#imgCardInfo2');
const titleCardInfo2 = document.querySelector('#titleCardInfo2');
const artistCardInfo2 = document.querySelector('#artistCardInfo2');
const imgCardInfo3 = document.querySelector('#imgCardInfo3');
const titleCardInfo3 = document.querySelector('#titleCardInfo3');
const artistCardInfo3 = document.querySelector('#artistCardInfo3');
const imgCardInfo4 = document.querySelector('#imgCardInfo4');
const titleCardInfo4 = document.querySelector('#titleCardInfo4');
const artistCardInfo4 = document.querySelector('#artistCardInfo4');
const imgCardInfo5 = document.querySelector('#imgCardInfo5');
const titleCardInfo5 = document.querySelector('#titleCardInfo5');
const artistCardInfo5 = document.querySelector('#artistCardInfo5');
const imgCardInfo6 = document.querySelector('#imgCardInfo6');
const titleCardInfo6 = document.querySelector('#titleCardInfo6');
const artistCardInfo6 = document.querySelector('#artistCardInfo6');
const songContainer = document.querySelectorAll('songContainer');
const songId1 = document.querySelector('#song1');
const songId2 = document.querySelector('#song2');
const songId3 = document.querySelector('#song3');
const songId4 = document.querySelector('#song4');
const songId5 = document.querySelector('#song5');
const songId6 = document.querySelector('#song6');
const sliderValue = document.querySelector('.sliderValue');


//Song Titles
//const songs = getItems();
//var path = window.location.pathname;
//var page = path.split("/").pop();

//Keep track of songs
let songIndex = 0

//Initially load song info DOM
let songs;
loadSong();
loadMainSongs();

//Update song details
async function loadSong() {
    songs = await getSongs();
    song = songs[songIndex];
    title.innerText = song['title']
    audio.src = `../music/${song['songMP3']}`
    cover.src = song['imageSongURL']
}

async function loadMainMenuSong(id) {
    songs = await getSongs();
    song = songs[id];
    title.innerText = song['title']
    audio.src = `../music/${song['songMP3']}`
    cover.src = song['imageSongURL']
    playSong();
}

async function loadMainSongs() {
    let imgCardInfo = [imgCardInfo1, imgCardInfo2, imgCardInfo3, imgCardInfo4, imgCardInfo5, imgCardInfo6]
    let titleCardInfo = [titleCardInfo1, titleCardInfo2, titleCardInfo3, titleCardInfo4, titleCardInfo5, titleCardInfo6]
    let artistCardInfo = [artistCardInfo1, artistCardInfo2, artistCardInfo3, artistCardInfo4, artistCardInfo5, artistCardInfo6]
    let songIdArr = [songId1, songId2, songId3, songId4, songId5, songId6]
    songs = await getSongs();
    let songIds = [];
    let songId;
    for (let i = 0; i < 6; i++) {
        songId = Math.floor(Math.random() * (songs.length - 1));
        while (songIds.includes(songId)) {
            songId++;
        }        
        song = songs[songId];
        imgCardInfo[i].src = song['imageSongURL']
        titleCardInfo[i].innerText = song['title']
        artistCardInfo[i].innerText = song['album']['artist']['fName']
        songIdArr[i].id = songId;
        songIds.push(songId);
    }
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

function muteSong() {
    musicContainer.classList.add('muted')
    muteBtn.querySelector('i.fas').classList.remove('fa-volume-high')
    muteBtn.querySelector('i.fas').classList.add('fa-volume-xmark')
    audio.muted = true;
    slider.value = audio.volume;
    progressBar.value = audio.volume;
    sliderValue.innerText = 0;
    //slider.value = 0;
    //progressBar.value = 0;
}

function unmuteSong() {
    musicContainer.classList.remove('muted')
    muteBtn.querySelector('i.fas').classList.add('fa-volume-high')
    muteBtn.querySelector('i.fas').classList.remove('fa-volume-xmark')

    audio.muted = false;
    slider.value = audio.volume * 100;
    progressBar.value = audio.volume * 100;
    sliderValue.innerText = audio.volume * 100;
    //slider.value = 50;
    //progressBar.value = 50;
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

    audio.currentTime = (clickX / width) * duration
}

slider.oninput = function () {
    progressBar.value = slider.value;
    
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

muteBtn.addEventListener('click', () => {
    const isMuted = musicContainer.classList.contains('muted')
    const volume = audio.volume;

    if (isMuted) {
        unmuteSong()
    } else {
        muteSong()
    }
})

//Change song events

prevBtn.addEventListener('click', prevSong);
nextBtn.addEventListener('click', nextSong);

audio.addEventListener('timeupdate', updateProgress);

progressContainer.addEventListener('click', setProgress)
audio.addEventListener('ended', nextSong)


