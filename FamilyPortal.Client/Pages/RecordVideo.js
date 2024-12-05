let mediaRecorder;
let recordedBlobs;

function handleDataAvailable(event) {
    if (event.data && event.data.size > 0) {
        recordedBlobs.push(event.data);
    }
}

export async function startRecording() {
    const constraints = {
        video: true,
        audio: true
    };
    
    const stream = await navigator.mediaDevices.getUserMedia(constraints);
    const videoElement = document.getElementById('video');
    videoElement.srcObject = stream;
    
    recordedBlobs = [];
    mediaRecorder = new MediaRecorder(stream);
    mediaRecorder.ondataavailable = handleDataAvailable;
    mediaRecorder.start();
    console.log('Recording started');
}

export function stopRecording() {
    mediaRecorder.stop();
    console.log('Recording stopped');
    const blob = new Blob(recordedBlobs, { type: 'video/webm' });
    const url = window.URL.createObjectURL(blob);
    const videoElement = document.getElementById('video');
    videoElement.srcObject = null;
    videoElement.src = url;
    downloadRecordedVideo(blob);
}

function downloadRecordedVideo(blob) {
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.style.display = 'none';
    a.href = url;
    a.download = 'recorded-video.webm';
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);
}
