var callback = function() {
    var elements = document.getElementsByClassName("opblock-summary-description");

    for (const summaryDescription of elements) {
        const match = summaryDescription.textContent.matchAll(/\[.*?\]/g);
        const array = [...match];

        if (!array[0]) {
            continue;
        }

        const matchText = summaryDescription.textContent.split("] ").at(-1);
        const arrayElement = [];
        
        array.forEach(m => {
            var dataEl = m["0"].replace("[", "").replace("]", "").replace(" ", "").split(":");

            var element = `
                <div class="swagger-bracket swagger-bracket-${dataEl[0].replace("#", "")}">${dataEl[1]}</div>
            `;

            arrayElement.push(element);
        });

        var htmlNewTag = `
            <div class="swagger-badges-content-block">
                ${arrayElement.join(" ")}
                <p class="opblock-summary-description">${matchText}</p>
            </div>
        `

        summaryDescription.outerHTML = htmlNewTag;
    }
};

// Repeat just in case page is slow to load.
for (let attempts = 1; attempts <= 2; attempts++) {
    setTimeout(callback, attempts * 700);
}

document.onreadystatechange = () => {
    if (document.readyState === 'complete') {
        document.body.addEventListener('click', () => {
            callback();
        }); 
    }
  };
