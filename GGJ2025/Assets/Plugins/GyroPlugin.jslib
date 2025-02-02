mergeInto(LibraryManager.library, {
    GetDeviceOrientation: function() {
        if (typeof window.orientationData === 'undefined') {
            window.orientationData = { alpha: 0, beta: 0, gamma: 0 };
            window.addEventListener('deviceorientation', function(event) {
                window.orientationData.alpha = event.alpha || 0;
                window.orientationData.beta = event.beta || 0;
                window.orientationData.gamma = event.gamma || 0;
            });
        }
    },
    GetAlpha: function() {
        return window.orientationData.alpha;
    },
    GetBeta: function() {
        return window.orientationData.beta;
    },
    GetGamma: function() {
        return window.orientationData.gamma;
    }
});