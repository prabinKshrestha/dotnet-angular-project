

export interface ATThemeModel {
    displayName: string;
    themeName: string;
    primaryColor: string;
    accentColor: string;
    isDefault: boolean;
}

export const ATThemes: ATThemeModel[] = [
    {
        displayName: "Pink / Green",
        themeName: "",
        primaryColor: '#e91e63',
        accentColor: '#4caf50',
        isDefault: true
    },
    {
        displayName: "Blue / Orange",
        themeName: "at-blue-orange-theme",
        primaryColor: '#03a9f4',
        accentColor: '#ff5722',
        isDefault: false
    },
    {
        displayName: "Dark Blue / Brown",
        themeName: "at-darkblue-brown-theme",
        primaryColor: '#1b2a4e',
        accentColor: '#4e3f1b',
        isDefault: false
    }
]