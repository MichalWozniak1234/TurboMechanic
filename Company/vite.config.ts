import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  root: "ClientApp",
  plugins: [react()],
  build: {
    outDir: "../Company.PortalWWW/wwwroot/dist",
    emptyOutDir: true,
    rollupOptions: {
      input: "ClientApp/src/main.tsx",
      output: {
        entryFileNames: "app.js",
        chunkFileNames: "chunks/[name].js",
        assetFileNames: "assets/[name][extname]",
      },
    },
  },
});
