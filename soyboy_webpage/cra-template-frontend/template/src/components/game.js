import { Unity, useUnityContext } from "react-unity-webgl";
import "../templates/game.css";
import "../templates/App.css"

export default function Game() {

    const {
        unityProvider,
        isLoaded,
        loadingProgression,
        requestFullscreen,
    } = useUnityContext({
        loaderUrl: "/unitybuild/soyboy.loader.js",
        dataUrl: '/unitybuild/soyboy.data',
        frameworkUrl: '/unitybuild/soyboy.framework.js',
        codeUrl: '/unitybuild/soyboy.wasm',
        webglContextAttributes: {
            preserveDrawingBuffer: true,
        },
    });

    const handleClickFullscreen = () => {
        if (isLoaded === false) {
            return;
        }
        requestFullscreen(true);
    };

    return (
            <div className='container'>
                <h1 className='Game-title'>SoyBoy</h1>
                <a className="comment-link" href={'/comment/'}>Comment on Beta</a>
                <div className='unityWrapper'>
                    {isLoaded === false && (
                    <div className='loadingBar'>
                        <div
                        className='loadingBarFill'
                        style={{ width: loadingProgression * 100 }}
                        />
                    </div>
                    )}
                    <Unity
                    unityProvider={unityProvider}
                    style={{ display: isLoaded ? "block" : "none" }}
                    />
                </div>
                <div id="footer">
                    <div id="unity-fullscreen-button" onClick={handleClickFullscreen}></div>
                </div>
            </div>
        );
};


