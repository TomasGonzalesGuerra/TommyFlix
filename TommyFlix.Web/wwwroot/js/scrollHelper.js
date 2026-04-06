window.scrollHelper = {
    _dotnet: null,
    _handler: null,

    init(dotnet) {
        this._dotnet = dotnet;
        this._handler = () => {
            const scrolled = window.scrollY > 60;
            dotnet.invokeMethodAsync('OnScroll', scrolled);
        };
        window.addEventListener('scroll', this._handler);
    },

    dispose() {
        if (this._handler)
            window.removeEventListener('scroll', this._handler);
    }
};