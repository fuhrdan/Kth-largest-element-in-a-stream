//*****************************************************************************
//** 703. Kth Largest Element in a Stream  leetcode                          **
//*****************************************************************************
//*****************************************************************************

typedef struct
{
    int* heap;
    int heapSize;
    int k;
} KthLargest;

void heapifyDown(int* heap, int idx, int size)
{
    int smallest = idx;
    int left = 2 * idx + 1;
    int right = 2 * idx + 2;

    if(left < size && heap[left] < heap[smallest])
    {
        smallest = left;
    }

    if(right < size && heap[right] < heap[smallest])
    {
        smallest = right;
    }

    if(smallest != idx)
    {
        int temp = heap[idx];
        heap[idx] = heap[smallest];
        heap[smallest] = temp;
        heapifyDown(heap, smallest, size);
    }
}

void heapifyUp(int* heap, int idx)
{
    int parent = (idx - 1) / 2;

    while(idx > 0 && heap[idx] < heap[parent])
    {
        int temp = heap[idx];
        heap[idx] = heap[parent];
        heap[parent] = temp;

        idx = parent;
        parent = (idx - 1) / 2;
    }
}

KthLargest* kthLargestCreate(int k, int* nums, int numsSize)
{
    KthLargest* obj = (KthLargest*)malloc(sizeof(KthLargest));
    obj->heap = (int*)malloc(sizeof(int) * k);
    obj->heapSize = 0;
    obj->k = k;

    for(int i = 0; i < numsSize; i++)
    {
        kthLargestAdd(obj, nums[i]);
    }

    return obj;
}

int kthLargestAdd(KthLargest* obj, int val)
{
    if(obj->heapSize < obj->k)
    {
        obj->heap[obj->heapSize++] = val;
        heapifyUp(obj->heap, obj->heapSize - 1);
    }
    else if(val > obj->heap[0])
    {
        obj->heap[0] = val;
        heapifyDown(obj->heap, 0, obj->heapSize);
    }

    return obj->heap[0];
}

void kthLargestFree(KthLargest* obj)
{
    free(obj->heap);
    free(obj);
}

/**
 * Your KthLargest struct will be instantiated and called as such:
 * KthLargest* obj = kthLargestCreate(k, nums, numsSize);
 * int param_1 = kthLargestAdd(obj, val);
 
 * kthLargestFree(obj);
*/