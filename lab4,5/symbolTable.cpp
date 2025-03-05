#include <iostream>
#include <list>
#include <string>

using namespace std;

// Size of the hash table
const int TABLE_SIZE = 128;

// Class to represent a symbol entry
class SymbolEntry {
public:
    string name;
    string type;
    int value;

    SymbolEntry(string name, string type, int value) : name(name), type(type), value(value) {}
};

// Class to represent the symbol table
class SymbolTable {
private:
    list<SymbolEntry>* table;

    // Hash function to compute the index for a given key
    int hashFunction(const string& key) {
        int hash = 0;
        for (char ch : key) {
            hash += ch; // Simple hash function: sum of ASCII values
        }
        return hash % TABLE_SIZE;
    }

public:
    SymbolTable() {
        table = new list<SymbolEntry>[TABLE_SIZE];
    }

    ~SymbolTable() {
        delete[] table;
    }

    // Insert a symbol into the symbol table
    void insert(const string& name, const string& type, int value) {
        int index = hashFunction(name);
        for (auto& entry : table[index]) {
            if (entry.name == name) {
                cout << "Symbol '" << name << "' already exists in the symbol table." << endl;
                return;
            }
        }
        table[index].push_back(SymbolEntry(name, type, value));
        cout << "Inserted symbol: " << name << " (" << type << ", " << value << ")" << endl;
    }

    // Lookup a symbol in the symbol table
    SymbolEntry* lookup(const string& name) {
        int index = hashFunction(name);
        for (auto& entry : table[index]) {
            if (entry.name == name) {
                return &entry;
            }
        }
        return nullptr; // Symbol not found
    }

    // Delete a symbol from the symbol table
    void remove(const string& name) {
        int index = hashFunction(name);
        auto& chain = table[index];
        for (auto it = chain.begin(); it != chain.end(); ++it) {
            if (it->name == name) {
                chain.erase(it);
                cout << "Deleted symbol: " << name << endl;
                return;
            }
        }
        cout << "Symbol '" << name << "' not found in the symbol table." << endl;
    }

    // Display the symbol table
    void display() {
        cout << "Symbol Table:" << endl;
        for (int i = 0; i < TABLE_SIZE; ++i) {
            if (!table[i].empty()) {
                cout << "Index " << i << ": ";
                for (const auto& entry : table[i]) {
                    cout << "[" << entry.name << ", " << entry.type << ", " << entry.value << "] ";
                }
                cout << endl;
            }
        }
    }
};

int main() {
    SymbolTable symbolTable;

    // Insert symbols
    symbolTable.insert("x", "int", 10);
    symbolTable.insert("y", "float", 20);
    symbolTable.insert("z", "int", 30);

    // Lookup symbols
    SymbolEntry* entry = symbolTable.lookup("x");
    if (entry) {
        cout << "Found symbol: " << entry->name << " (" << entry->type << ", " << entry->value << ")" << endl;
    } else {
        cout << "Symbol 'x' not found." << endl;
    }

    // Delete a symbol
    symbolTable.remove("y");

    // Display the symbol table
    symbolTable.display();

    return 0;
}