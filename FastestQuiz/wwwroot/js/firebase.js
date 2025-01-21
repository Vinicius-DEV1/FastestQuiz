// Importação dos módulos necessários do Firebase
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.0.0/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/9.0.0/firebase-analytics.js";

console.log("Carregando configurações do Firebase...");
// Buscando as configurações do Firebase do servidor
fetch("/api/firebase-config")
    .then(response => {
        if (!response.ok) {
            throw new Error("Erro ao buscar as configurações do Firebase");
        }
        return response.json();
    })
    .then(firebaseConfig => {
        // Inicializar o Firebase com as configurações recebidas
        const app = initializeApp(firebaseConfig);
        const analytics = getAnalytics(app);

        console.log("Firebase Analytics Initialized.");
    })
    .catch(error => {
        console.error("Erro ao inicializar o Firebase:", error);
    });
