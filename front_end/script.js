document.getElementById("bikeForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const registerNumber = document.getElementById("registerNumber").value;
    const brand = document.getElementById("brand").value;
    const model = document.getElementById("model").value;
    const category = document.getElementById("category").value;
    const imageFiles = document.getElementById("imageFiles").files;  // Get multiple files
    const availabilityStatus = document.getElementById("availabilityStatus").checked;

    const formData = new FormData();
    formData.append("RegisterNumber", registerNumber);
    formData.append("Brand", brand);
    formData.append("Model", model);
    formData.append("Category", category);

    // Append all the selected images
    for (let i = 0; i < imageFiles.length; i++) {
        formData.append("ImageUrls", imageFiles[i]);
    }

    formData.append("AvailabilityStatus", availabilityStatus);

    try {
        const response = await fetch("http://localhost:5205/api/Motorbike/AddBike", {
            method: "POST",
            body: formData,
        });

        if (response.ok) {
            const data = await response.json();
            console.log("Bike added successfully:", data);
        } else {
            const error = await response.json();
            console.error("Error adding bike:", error);
        }
    } catch (error) {
        console.error("Network error:", error);
    }
});






 
