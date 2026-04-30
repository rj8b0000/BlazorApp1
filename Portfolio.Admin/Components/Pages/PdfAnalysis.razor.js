export function initSplitter() {
    const container = document.querySelector('.pdf-analysis-container');
    if (!container || container.dataset.splitterInitialized) return;
    container.dataset.splitterInitialized = 'true';
    
    const splitter = document.querySelector('.splitter');
    if (!splitter) return;
    
    let isResizing = false;

    splitter.addEventListener('mousedown', function(e) {
        isResizing = true;
        document.body.style.cursor = 'col-resize';
        // Disable pointer events on iframes so mousemove works smoothly over the PDF
        const iframes = document.querySelectorAll('iframe');
        iframes.forEach(i => i.style.pointerEvents = 'none');
        e.preventDefault(); // Prevent text selection
    });

    document.addEventListener('mousemove', function(e) {
        if (!isResizing) return;
        
        const containerRect = container.getBoundingClientRect();
        // Calculate percentage based on mouse position relative to container
        let newWidthPercentage = ((e.clientX - containerRect.left) / containerRect.width) * 100;
        
        // Constrain between 20% and 80%
        if (newWidthPercentage < 20) newWidthPercentage = 20;
        if (newWidthPercentage > 80) newWidthPercentage = 80;
        
        container.style.setProperty('--left-width', newWidthPercentage + '%');
    });

    document.addEventListener('mouseup', function() {
        if (isResizing) {
            isResizing = false;
            document.body.style.cursor = '';
            // Re-enable pointer events on iframes
            const iframes = document.querySelectorAll('iframe');
            iframes.forEach(i => i.style.pointerEvents = '');
        }
    });
}
