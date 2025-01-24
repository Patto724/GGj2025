mergeInto(LibraryManager.library, {
    GetDeviceOrientation: function() {
        if (typeof window.orientationData === 'undefined') {
            window.orientationData = { alpha: 0, beta: 0, gamma: 0 };
            window.addEventListener('deviceorientation', function(event) {
                window.orientationData.alpha = event.alpha;
                window.orientationData.beta = event.beta;
                window.orientationData.gamma = event.gamma;
            });
        }
        return window.orientationData;
    }
});