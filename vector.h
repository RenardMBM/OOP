#include <iostream>

template<typename T=int>
class Vector {
private:
    template<bool IsConst>
    class BaseIterator {
    private:
        typename std::conditional_t<IsConst, const T*, T*> current;
    public:
        using pointer = typename std::conditional_t<IsConst, T const*, T*>;
        using reference = typename std::conditional_t<IsConst, T const&, T&>;
        using difference_type = ptrdiff_t;

        BaseIterator(T *curr) : current(curr) {}

        BaseIterator &operator=(const BaseIterator &other) {
            if (this != &other) {
                current = other.current;
            }
            return *this;
        }

        BaseIterator &operator++() {
            ++current;
            return *this;
        }

        BaseIterator operator++(int i) {
            BaseIterator tmp(current);
            ++current;
            return tmp;
        }

        BaseIterator &operator--() {
            --current;
            return *this;
        }

        BaseIterator operator--(int i) {
            BaseIterator tmp(current);
            ++current;
            return tmp;
        }

        bool operator==(const BaseIterator &other) {
            return current == other.current;
        }

        bool operator!=(const BaseIterator &other) {
            return !(*this == other);
        }

        reference operator*() {
            return *current;
        }

        pointer operator->() {
            return current;
        }
    };
    T* vec = nullptr;
    size_t sz;
    size_t memory;

    friend Vector<T> operator+(T l, const Vector<T>& v);

    void _more_memory() {
        Vector new_one(memory * 2, '0');
        for (size_t i = 0; i < sz; ++i)
            new_one.vec[i] = vec[i];
        new_one.sz = sz;
        new_one.swap(*this);
    }

    void _mergeTo(Vector<T> &res, const Vector<T> &v1, const Vector<T> &v2) {
        auto siz1 = v1.size();
        auto siz2 = v2.size();
        size_t p1 = 0;
        size_t p2 = 0;

        while (p1 < siz1 && p2 < siz2) {
            if (v1[p1] < v2[p2])
                res.push_back(v1[p1++]);
            else
                res.push_back(v2[p2++]);
        }

        while (p1 < siz1) res.push_back(v1[p1++]);
        while (p2 < siz2) res.push_back(v2[p2++]);
        std::cout << "После слияния:\n";
        std::cout << "res (" << res.size() << ") :";
        for (auto i: res){
            std::cout << i <<" ";
        }
        std::cout << std::endl;
    }
    void _mergeSort(Vector<T> &v) {
        if (v.size() <= 1)
            return;

        int count = v.size() / 2;
        Vector<T> v1 = v.subVec(0, count);
        Vector<T> v2 = v.subVec(count, v.size()-count);

        std::cout << "Деление:\n";
        std::cout << "v1 (" << v1.size() << ") :";
        for (auto i: v1){
            std::cout << i <<" ";
        }
        std::cout << std::endl;
        std::cout << "v2 (" << v2.size() << ") :";
        for (auto i: v2){
            std::cout << i <<" ";
        }
        std::cout << std::endl;

        _mergeSort(v1);
        _mergeSort(v2);
        std::cout << "После сортировки:\n";
        std::cout << "v1 (" << v1.size() << ") :";
        for (auto i: v1){
            std::cout << i <<" ";
        }
        std::cout << std::endl;
        std::cout << "v2 (" << v2.size() << ") :";
        for (auto i: v2){
            std::cout << i <<" ";
        }
        std::cout << std::endl;
        v.clear();
        _mergeTo(v, v1, v2);
        v1.clear(); v2.clear();
    }
    void _merge(const Vector<T> &v1, const Vector<T> &v2) {
        auto siz1 = v1.size();
        auto siz2 = v2.size();
        size_t p1 = 0;
        size_t p2 = 0;

        while (p1 < siz1 && p2 < siz2) {
            if (v1[p1] < v2[p2])
                push_back(v1[p1++]);
            else
                push_back(v2[p2++]);
        }

        while (p1 < siz1)
            push_back(v1[p1++]);
        while (p2 < siz2)
            push_back(v2[p2++]);

    }
public:

    typedef T* pointer;
    typedef const T* const_pointer;
    typedef T& reference;
    typedef const T& const_reference;
    typedef T value_type;

    typedef BaseIterator<false> iterator;
    typedef BaseIterator<true> const_iterator;

    Vector() : vec(new T[2]), sz(0), memory(2) {}
    Vector(size_t n, T c) : vec(new T[n]), sz(n), memory(n) {
        memset(vec, c, n);
    }
    Vector(size_t n) : vec(new T[n]), sz(n), memory(n) {
        memset(vec, 0, n);
    }
    Vector(const_pointer s) : vec(new T[std::size(s)]), sz(std::size(s)), memory(std::size(s)) {
        for (size_t i = 0; i < sz; i++) vec[i] = s[i];
    }
    Vector(const Vector& v) : vec(new T[v.sz]), sz(v.sz), memory(v.sz) {
        memcpy(vec, v.vec, sz);
    }

    size_t size() const {
        return sz;
    }

    void swap(Vector& s) {
        std::swap(vec, s.vec);
        std::swap(sz, s.sz);
        std::swap(memory, s.memory);
    }

    Vector& operator=(Vector s) {
        swap(s);
        return *this;
    }

    Vector& operator+=(T addit) {
        push_back(addit);
        return *this;
    }

    bool operator==(const Vector& s) {
        if (sz != s.sz) return false;
        else {
            for (size_t i = 0; i < sz; ++i) {
                if (vec[i] != s.vec[i]) return false;
            }
        }
        return true;
    }

    void push_back(T addit) {
        if (sz >= memory)
            _more_memory();
        vec[sz] = addit;
        sz++;
    }

    Vector operator+(T addit) const {
        Vector res(*this);
        res += addit;
        return res;
    }

    Vector& operator+=(const Vector& s) {
        while (memory <= s.sz + sz)
            _more_memory();
        for (size_t i = sz; i < sz + s.sz; ++i) {
            vec[i] = s.vec[i - sz];
        }
        sz += s.sz;
        return *this;
    }

    Vector operator+(const Vector& s) const {
        Vector res(*this);
        res += s;
        return res;
    }

    void pop_back() {
        if (sz)
            sz--;
    }

    reference operator[](size_t ind) { return vec[ind]; }

    const_reference operator[](size_t ind) const { return vec[ind]; }

    void clear() {
        delete[] vec;
        sz = 0;
        memory = 0;
        vec = nullptr;
    }

    bool empty() {
        return sz == 0;
    }

    Vector subVec(size_t begin, size_t count) const {
        Vector<T> sub(count);
        for (size_t i = begin; i < begin + count; ++i) sub[i - begin] = vec[i];
        return sub;
    }

    size_t find(const Vector& s) const {
        size_t leng = s.sz;
        for (size_t i = 0; i <= sz - leng; ++i) {
            if (this->subVec(i, leng) == s) return i;
        }
        return sz;
    }

    size_t rfind(const Vector& s) const {
        size_t len = s.sz;
        size_t res = s.sz;
        for (size_t i = 0; i <= sz - len; ++i) {
            if (this->subVec(i, len) == s) res = i;
        }
        return res;
    }

    iterator begin() { return iterator(vec); }
    const_iterator begin() const{ return const_iterator(vec); }
    const_iterator cbegin() const{ return const_iterator(vec); }

    iterator end() { return iterator(vec + sz); }
    const_iterator end() const { return const_iterator(vec + sz); }
    const_iterator cend() const { return const_iterator(vec + sz); }

    using reverse_iterator = std::reverse_iterator<iterator>;
    using const_reverse_iterator = std::reverse_iterator<const_iterator>;

    reverse_iterator rbegin() { return std::reverse_iterator(end()); }

    reverse_iterator rend() { return std::reverse_iterator(begin()); }

    const_reverse_iterator rbegin() const { return std::reverse_iterator(cend()); }

    const_reverse_iterator rend() const { return std::reverse_iterator(cbegin()); }

    const_reverse_iterator crbegin() const { return std::reverse_iterator(cend()); }

    const_reverse_iterator crend() const { return std::reverse_iterator(cbegin()); }


    void mergeSort() {
        if (size() <= 1)
            return;

        int count = size() / 2;
        Vector<T> v1 = subVec(0, count);
        Vector<T> v2 = subVec(count, size()-count);

        std::cout << "Деление:\n";
        std::cout << "v1 (" << v1.size() << ") :";
        for (auto i: v1){
            std::cout << i <<" ";
        }
        std::cout << std::endl;
        std::cout << "v2 (" << v2.size() << ") :";
        for (auto i: v2){
            std::cout << i <<" ";
        }
        std::cout << std::endl;

        _mergeSort(v1);
        _mergeSort(v2);

        std::cout << "После сортировки:\n";
        std::cout << "v1 (" << v1.size() << ") :";
        for (auto i: v1){
            std::cout << i <<" ";
        }
        std::cout << std::endl;
        std::cout << "v2 (" << v2.size() << ") :";
        for (auto i: v2){
            std::cout << i <<" ";
        }
        std::cout << std::endl;

        clear();
        _merge(v1, v2);
        v1.clear(); v2.clear();
    }
};


template<typename Type>
Vector<Type> operator+(Type l, const Vector<Type>& v) {
    return Vector(1, l) + v;
}

template<typename Type>
void swap(Vector<Type>& left, Vector<Type>& right) {
    left.swap(right);
}
