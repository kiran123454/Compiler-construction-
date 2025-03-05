
#include <windows.h>
#include <string>
#include <vector>
#include <cctype>

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;

public ref class MainForm : public Form {
private:
    TextBox^ inputTextBox;
    Button^ analyzeButton;
    RichTextBox^ outputTextBox;

public:
    MainForm(void) {
        InitializeComponent();
    }

private:
    void InitializeComponent(void) {
        this->inputTextBox = gcnew TextBox();
        this->analyzeButton = gcnew Button();
        this->outputTextBox = gcnew RichTextBox();

        // Input TextBox
        this->inputTextBox->Location = Point(20, 20);
        this->inputTextBox->Multiline = true;
        this->inputTextBox->Size = System::Drawing::Size(400, 100);

        // Analyze Button
        this->analyzeButton->Location = Point(20, 130);
        this->analyzeButton->Text = "Analyze";
        this->analyzeButton->Click += gcnew EventHandler(this, &MainForm::OnAnalyzeButtonClick);

        // Output RichTextBox
        this->outputTextBox->Location = Point(20, 180);
        this->outputTextBox->Size = System::Drawing::Size(400, 200);
        this->outputTextBox->ReadOnly = true;

        // Form Settings
        this->Text = "Lexical Analyzer";
        this->Size = System::Drawing::Size(450, 450);
        this->Controls->Add(this->inputTextBox);
        this->Controls->Add(this->analyzeButton);
        this->Controls->Add(this->outputTextBox);
    }

    void OnAnalyzeButtonClick(Object^ sender, EventArgs^ e) {
        String^ input = inputTextBox->Text;
        outputTextBox->Clear();

        std::string inputStr = msclr::interop::marshal_as<std::string>(input);
        std::vector<std::string> tokens = Tokenize(inputStr);

        for (const std::string& token : tokens) {
            outputTextBox->AppendText(gcnew String(token.c_str()) + Environment::NewLine);
        }
    }

    std::vector<std::string> Tokenize(const std::string& input) {
        std::vector<std::string> tokens;
        std::string token;
        for (size_t i = 0; i < input.length(); ++i) {
            char ch = input[i];

            // Skip whitespace
            if (isspace(ch)) {
                if (!token.empty()) {
                    tokens.push_back(token);
                    token.clear();
                }
                continue;
            }

            // Handle identifiers and keywords
            if (isalpha(ch) || ch == '_') {
                token += ch;
                while (i + 1 < input.length() && (isalnum(input[i + 1]) || input[i + 1] == '_')) {
                    token += input[++i];
                }
                tokens.push_back(token);
                token.clear();
                continue;
            }

            // Handle numbers
            if (isdigit(ch)) {
                token += ch;
                while (i + 1 < input.length() && isdigit(input[i + 1])) {
                    token += input[++i];
                }
                tokens.push_back(token);
                token.clear();
                continue;
            }

            // Handle single-character tokens
            token += ch;
            tokens.push_back(token);
            token.clear();
        }

        if (!token.empty()) {
            tokens.push_back(token);
        }

        return tokens;
    }
};

[STAThread]
int main(array<String^>^ args) {
    Application::EnableVisualStyles();
    Application::SetCompatibleTextRenderingDefault(false);

    Application::Run(gcnew MainForm());
    return 0;
}