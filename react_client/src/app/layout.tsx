import type { Metadata } from "next";
import { Inter } from "next/font/google";
import 'bootstrap/dist/css/bootstrap.css';
import "./globals.css";
import Header from "@/components/nav/Header";
import Footer from "@/components/nav/Footer";
import BootstrapActivation from "@/components/BootstrapActivation";
import AppState from "@/components/AppState";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
    title: "Next App",
    description: "Study planner react client",
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en">
            <body className={inter.className}>
                <AppState>
                    <Header />

                    <div className="container">
                        <main role="main" className="pb-3">
                            {children}
                        </main>
                    </div>

                    <Footer />

                    <BootstrapActivation />
                </AppState>
            </body>
        </html>
    );
}
